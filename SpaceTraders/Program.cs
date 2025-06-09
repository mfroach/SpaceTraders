namespace SpaceTraders;

using System;
using SpaceTraders.Models;

class Program {
    static async Task Main(string[] args) {
        // todo need to catch index out of range when no arg passed
        // can we take stdin instead of arg? and/or pass filename?
        var httpClientService = new HttpClientService(args[0]);

        while (true) {
            Console.WriteLine("Enter a command (e.g., 'agent', 'loc', 'contracts') or 'exit' to quit:");
            var command = Console.ReadLine()?.Trim().ToLowerInvariant();

            if (command == "exit") {
                break;
            }

            switch (command) {
                case "account":
                    Account? account = await httpClientService.GetAccountAsync();
                    if (account != null) {
                        if (!SQLBoy.accountExists(account)) {
                            Console.WriteLine(SQLBoy.insertAccount(account));
                        }

                        Console.WriteLine("Account Details:");
                        Console.WriteLine($"  Account ID: {account.Id}\n" +
                                          $"  Account Email: {account.Email}\n" +
                                          $"  Account Created At: {account.CreatedAt}");
                    }

                    break;
                case "agent":
                    Agent? agent = await httpClientService.GetAgentAsync();
                    if (agent != null) {
                        if (!SQLBoy.agentExists(agent)) {
                            Console.WriteLine(SQLBoy.insertAgent(agent));
                        }

                        Console.WriteLine("Agent Details:");
                        Console.WriteLine($"  Symbol: {agent.Symbol}");
                        Console.WriteLine($"  Credits: {agent.Credits}");
                        Console.WriteLine($"  Headquarters: {agent.Headquarters}");
                        Console.WriteLine($"  Faction: {agent.StartingFaction}");
                        Console.WriteLine($"  Ship Count: {agent.ShipCount}");
                        Console.WriteLine($"  Account ID: {agent.AccountId}");
                    } else {
                        Console.WriteLine("Failed to retrieve or deserialize agent data.");
                    }

                    break;
                case "way": {
                    Console.Write("Enter waypoint symbol: ");
                    string? subcommand = Console.ReadLine()?.Trim().ToLowerInvariant();
                    Deserializer.Waypoint? location = await httpClientService.GetWaypointAsync(subcommand);
                    if (location != null) {
                        Console.WriteLine("Waypoint Details:");
                        Console.WriteLine($"  Symbol: {location.Symbol}");
                        Console.WriteLine($"  Type: {location.Type}");
                        Console.WriteLine($"  System Symbol: {location.SystemSymbol}");
                        Console.WriteLine($"  Coordinates: X={location.X}, Y={location.Y}");
                        Console.WriteLine($"  Is Under Construction: {location.IsUnderConstruction}");

                        if (location.Faction != null) {
                            Console.WriteLine($"  Faction Symbol: {location.Faction.Symbol}");
                        }

                        Console.WriteLine("  Orbitals:");
                        if (location.Orbitals != null && location.Orbitals.Length > 0) {
                            foreach (var orbital in location.Orbitals) {
                                Console.WriteLine($"    - {orbital.Symbol}");
                            }
                        } else {
                            Console.WriteLine("    None");
                        }

                        Console.WriteLine("  Traits:");
                        if (location.Traits != null && location.Traits.Length > 0) {
                            foreach (var trait in location.Traits) {
                                Console.WriteLine($"    - {trait.Name} ({trait.Symbol}): {trait.Description}");
                            }
                        } else {
                            Console.WriteLine("    None");
                        }

                        if (location.Chart != null) {
                            Console.WriteLine("  Chart Details:");
                            Console.WriteLine($"    Waypoint Symbol: {location.Chart.WaypointSymbol}");
                            Console.WriteLine($"    Submitted By: {location.Chart.SubmittedBy}");
                            Console.WriteLine($"    Submitted On: {location.Chart.SubmittedOn}");
                        }

                        // Modifiers can be complex, printing count for now
                        if (location.Modifiers != null) {
                            Console.WriteLine($"  Modifiers Count: {location.Modifiers.Length}");
                        }
                    } else {
                        Console.WriteLine("Failed to retrieve or deserialize location data.");
                    }

                    break;
                }
                case "system": {
                    Console.Write("Enter system symbol: ");
                    string? subcommand = Console.ReadLine()?.Trim().ToUpper();
                    SystemDetails? system = await httpClientService.GetSystemAsync(subcommand);
                    if (system != null) {
                        Console.WriteLine("System Details:");
                        Console.WriteLine($"  Name: {system.Name}");
                        Console.WriteLine($"  Symbol: {system.Symbol}");
                        Console.WriteLine($"  Sector: {system.SectorSymbol}");
                        Console.WriteLine($"  Type: {system.Type}");
                        Console.WriteLine($"  Coordinates: X={system.X}, Y={system.Y}");
                        Console.WriteLine($"  Constellation: {system.Constellation}");
                        if (system.Waypoints != null) {
                            Console.WriteLine("List waypoints in system y/n?");
                            if (Console.ReadLine() == "y") {
                                foreach (var waypoint in system.Waypoints) {
                                    Console.WriteLine($"  Symbol: {waypoint.Symbol}\n" +
                                                      $"  Type: {waypoint.Type}\n" +
                                                      $"  Coordinates: X={waypoint.X} Y={waypoint.Y}");
                                    if (waypoint.Orbitals != null) {
                                        Console.WriteLine("-- Orbitals --");
                                        foreach (var orbital in waypoint.Orbitals) {
                                            Console.WriteLine($"  Symbol: {orbital.Symbol}");
                                        }

                                        Console.WriteLine("   --------");
                                    }
                                }
                            }
                        } else {
                            Console.WriteLine("No waypoints in system.");
                        }

                        break;
                    } else {
                        Console.WriteLine("No system found or error occurred.");
                        break;
                    }
                }

                case "contracts": {
                    // todo on enter 'contracts', only goto root shell when 'exit', probably don't use goto
                    Console.WriteLine("Contracts sub-commands: 'list', 'contract [ID]'");
                    string? subcommand = Console.ReadLine()?.Trim().ToLowerInvariant();

                    if (subcommand == "list") {
                        // Should I just do switch case here?
                        var contractsArray = await httpClientService.GetContractListAsync();

                        if (contractsArray != null && contractsArray.Length > 0) {
                            foreach (var contract in contractsArray) {
                                Console.WriteLine($"Contract ID: {contract.ContractID}");
                                Console.WriteLine($"    Faction: {contract.FactionSymbol}\n" +
                                                  $"    Type: {contract.ContractType}\n" +
                                                  $"    Deadline: {contract.Terms.Deadline}\n" +
                                                  $"    Payment on Accepted: {contract.Terms.Payment.OnAccepted}\n" +
                                                  $"    Payment on Fulfilled: {contract.Terms.Payment.OnFulfilled}\n" +
                                                  $"    Accepted: {contract.Accepted}");
                            }
                        } else {
                            Console.WriteLine("No contracts found or an error occurred.");
                        }
                    } else if (subcommand.Contains("contract")) {
                        // do we really want to be doing .contains? or branch
                        string contractID = subcommand.Substring(9);
                        Deserializer.Contract? contract = await httpClientService.GetContractAsync(contractID);
                        Console.WriteLine($"Contract ID: {contract.ContractID}");
                        Console.WriteLine($"    Faction: {contract.FactionSymbol}\n" +
                                          $"    Type: {contract.ContractType}\n" +
                                          $"    Deadline: {contract.Terms.Deadline}\n" +
                                          $"    Payment on Accepted: {contract.Terms.Payment.OnAccepted}\n" +
                                          $"    Payment on Fulfilled: {contract.Terms.Payment.OnFulfilled}\n" +
                                          $"    Accepted: {contract.Accepted}");
                    } else if (subcommand == "accept") {
                        Console.WriteLine("Enter contract ID:");
                        string contractID = Console.ReadLine();
                        try {
                            using var response = await httpClientService.AcceptContract(contractID);
                            response.EnsureSuccessStatusCode();
                        }
                        catch (HttpRequestException ex) {
                            Console.WriteLine($"Exception thrown on accepting contract: {ex.Message}");
                        }

                        Deserializer.Contract? contract = await httpClientService.GetContractAsync(contractID);
                        Console.WriteLine("-- Contract Accepted --");
                        Console.WriteLine($"Payment received: {contract.Terms.Payment.OnAccepted} ");
                        Console.WriteLine($"Contract ID: {contract.ContractID}");
                        Console.WriteLine($"    Faction: {contract.FactionSymbol}\n" +
                                          $"    Type: {contract.ContractType}\n" +
                                          $"    Deadline: {contract.Terms.Deadline}\n" +
                                          $"    Payment on Fulfilled: {contract.Terms.Payment.OnFulfilled}");
                    } else {
                        Console.WriteLine("Command unrecognized or error occurred.");
                    }

                    break;
                }
                case "ships": {
                    Console.WriteLine("Ships sub-commands: 'list', 'orbit'");
                    string? subcommand = Console.ReadLine()?.Trim().ToLowerInvariant();
                    switch (subcommand) {
                        case "list":
                            throw new NotImplementedException();
                        //Deserializer.ShipList? shipList = await httpClientService.GetShipListAsync();
                        case "orbit":
                            throw new NotImplementedException();
                        case "route":
                            throw new NotImplementedException();
                    }

                    throw new NotImplementedException();
                }
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }
}
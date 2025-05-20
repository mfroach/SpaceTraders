namespace Spacetraders;

using System;

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
                case "agent":
                    Deserializer.Agent? agent = await httpClientService.GetAgentAsync();
                    if (agent != null) {
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
                case "loc":
                    Deserializer.Waypoint? location = await httpClientService.GetLocationAsync();
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
                case "contracts":
                    // todo implement contract accept
                    // todo on enter 'contracts', only goto root shell when 'exit', probably don't use goto
                    Console.WriteLine("Contracts sub-commands: 'list', 'contract [ID]'");
                    var subcommand = Console.ReadLine()?.Trim().ToLowerInvariant();
                    
                    if (subcommand == "list") {
                        var contractsArray = await httpClientService.GetContractsAsync();

                        if (contractsArray != null && contractsArray.Length > 0) {
                            foreach (var contract in contractsArray) {
                                Console.WriteLine($"Contract ID: {contract.ContractID}");
                                Console.WriteLine($"    Faction: {contract.FactionSymbol}\n" +
                                                  $"    Type: {contract.ContractType}\n" +
                                                  $"    Deadline: {contract.Terms.Deadline}\n" +
                                                  $"    Payment on Accepted: {contract.Terms.Payment.OnAccepted}");

                            }
                        } else {
                            Console.WriteLine("No contracts found or an error occurred.");
                        }

                    } else if (subcommand.Contains("contract")) {
                        string contractID = subcommand.Substring(9);
                        Deserializer.Contract? contract = await httpClientService.GetContractAsync(contractID);
                        Console.WriteLine($"Contract ID: {contract.ContractID}");
                        Console.WriteLine($"    Faction: {contract.FactionSymbol}\n" +
                                          $"    Type: {contract.ContractType}\n" +
                                          $"    Deadline: {contract.Terms.Deadline}\n" +
                                          $"    Payment on Accepted: {contract.Terms.Payment.OnAccepted}");
                    }

                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }
}
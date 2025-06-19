using System.Net.Http.Headers;
using SpaceTraders.Http;
using SpaceTraders.Models;

namespace SpaceTraders;

class Program {
    static async Task Main(string[] args) {
        if (args.Length == 0 || string.IsNullOrWhiteSpace(args[0])) {
            Console.WriteLine("Error: Token is required as the first argument.");
            return;
        }

        string token = args[0];
        var httpClient = InitialiseHttpClient(token);
        //var sqlBoy = new SQLBoy();
        var locationService = new LocationService(httpClient);
        var agentService = new AgentService(httpClient);
        var accountService = new AccountService(httpClient);
        var contractService = new ContractService(httpClient);
        var shipService = new ShipService(httpClient);

        while (true) {
            Console.WriteLine("Enter a command or 'exit' to quit:");
            var command = Console.ReadLine()?.Trim().ToLowerInvariant();

            if (command == "exit") {
                break;
            }

            switch (command) {
                case "account":
                    await getAccount(accountService);
                    break;
                case "agent":
                    await getAgent(agentService);
                    break;
                case "way":
                    await waypointSubmenu(locationService);
                    break;
                case "system":
                    await systemSubmenu(locationService);
                    break;
                case "contracts":
                    await contractsSubmenu(contractService);
                    break;
                case "ships":
                    await shipsSubmenu(shipService);
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }

    private static HttpClient InitialiseHttpClient(string token) {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return httpClient;
    }

    static async Task getAccount(AccountService accountService) {
        Account? account = await accountService.GetAccountAsync();
        if (account != null) {
            //if (!sqlBoy.AccountExists(account)) {
            //    Console.WriteLine(sqlBoy.InsertAccount(account));
            //}

            Console.WriteLine("Account Details:");
            Console.WriteLine($"  Account ID: {account.Id}\n" +
                              $"  Account Email: {account.Email}\n" +
                              $"  Account Created At: {account.CreatedAt}");
        }
    }

    static async Task getAgent(AgentService agentService) {
        Agent? agent = await agentService.GetAgentAsync();
        if (agent != null) {
            //if (!sqlBoy.agentExists(agent)) {
            //    Console.WriteLine(sqlBoy.insertAgent(agent));
            //}

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
    }

    static async Task waypointSubmenu(LocationService locationService) {
        Console.Write("Enter waypoint symbol: ");
        string? subcommand = Console.ReadLine()?.Trim().ToLowerInvariant();
        Waypoint? location = await locationService.GetWaypointAsync(subcommand);
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
    }

    static async Task systemSubmenu(LocationService locationService) {
        Console.Write("Enter system symbol: ");
        string? subcommand = Console.ReadLine()?.Trim().ToUpper();
        SystemDetails? system = await locationService.GetSystemAsync(subcommand);
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
        } else {
            Console.WriteLine("No system found or error occurred.");
        }
    }

    static async Task shipsSubmenu(ShipService shipService) {
        Console.WriteLine("Ships sub-commands: 'list', 'orbit'");
        string? subcommand = Console.ReadLine()?.Trim().ToLowerInvariant();
        switch (subcommand) {
            case "list":
                Ship[]? shipList = await shipService.GetShipListAsync();
                foreach (var ship in shipList) {
                    Console.WriteLine($"Symbol: {ship.Symbol}");
                }

                break;
            case "orbit":
                throw new NotImplementedException();
            case "dock":
                throw new NotImplementedException();
            case "refuel":
                throw new NotImplementedException();
            case "route":
                throw new NotImplementedException();
        }
    }

    static async Task contractsSubmenu(ContractService contractService) {
        Console.WriteLine("Contracts sub-commands: 'list', 'contract [ID]', 'accept'");
        string subcommand = Console.ReadLine()?.Trim().ToLowerInvariant();

        if (subcommand == "list") {
            var contractsArray = await contractService.GetContractListAsync();

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
            string contractID = subcommand.Substring(9);
            Contract? contract = await contractService.GetContractAsync(contractID);
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
            if (contractID != null) {
                try {
                    using var response = await contractService.AcceptContractAsync(contractID);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException ex) {
                    Console.WriteLine($"Exception thrown on accepting contract: {ex.Message}");
                }
            } else {
                Console.WriteLine("No input.");
            }

            Contract? contract = await contractService.GetContractAsync(contractID);
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
    }
}
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
        var httpClient = BaseApiService.InitialiseHttpClient(token);
        //var sqlBoy = new SQLBoy();
        await UserMenu(httpClient);
    }
    
    static async Task UserMenu(HttpClient httpClient) {
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
                    await GetAccount(accountService);
                    break;
                case "agent":
                    await GetAgent(agentService);
                    break;
                case "way":
                    await WaypointSubmenu(locationService);
                    break;
                case "system":
                    await SystemSubmenu(locationService);
                    break;
                case "contracts":
                    await ContractsSubmenu(contractService);
                    break;
                case "ships":
                    await ShipsSubmenu(shipService);
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }

    static async Task GetAccount(AccountService accountService) {
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

    static async Task GetAgent(AgentService agentService) {
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

    static async Task WaypointSubmenu(LocationService locationService) {
        Console.Write("Enter subcommand (e.g. info, search) ");
        string? subcommand = Console.ReadLine()?.Trim().ToLowerInvariant();
        switch (subcommand) {
            case "info":
                string? wayInfo = Console.ReadLine()?.Trim().ToLowerInvariant();
                if (wayInfo != null) {
                    Waypoint? location = await locationService.GetWaypointAsync(wayInfo);
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
            case "search":
                Console.WriteLine("Enter system to search: ");
                string? searchSystem = Console.ReadLine()?.Trim().ToUpperInvariant();
                Console.WriteLine("Enter trait to search for: ");
                string? searchTrait = Console.ReadLine()?.Trim().ToUpperInvariant();
                Waypoint[]? searchLocation = await locationService.GetWaypointListByTraitAsync(searchSystem, searchTrait);
                foreach (var x in searchLocation) {
                    Console.WriteLine($"Waypoint: {x.Symbol}");
                }
                break;
        }
    }

    static async Task SystemSubmenu(LocationService locationService) {
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

    static async Task ShipsSubmenu(ShipService shipService) {
        Console.WriteLine("Ships sub-commands: 'list', 'orbit'");
        string? subcommand = Console.ReadLine()?.Trim().ToLowerInvariant();
        switch (subcommand) {
            case "list":
                Ship[]? shipList = await shipService.GetShipListAsync();
                foreach (var i in shipList) {
                    Console.WriteLine($"Symbol: {i.Symbol}");
                }

                break;
            case "info":
                Console.WriteLine("Enter ship symbol:");
                string shipSymbol = Console.ReadLine().ToUpper();
                Ship? ship = await shipService.GetShipAsync(shipSymbol);
                Console.WriteLine("Ship Details:\n" +
                                  $"    Ship Location: {ship.Nav.WaypointSymbol}\n" +
                                  $"    Ship Status: {ship.Nav.Status} - Mode: {ship.Nav.FlightMode}\n" +
                                  $"    Ship Role: {ship.Registration.Role}\n"
                );
                if (ship.Nav.Route.Origin.Symbol != ship.Nav.Route.Destination.Symbol) {
                    Console.WriteLine("Route:\n" +
                                      $"    Ship Origin: {ship.Nav.Route.Origin.Symbol}\n" +
                                      $"    Ship Destination: {ship.Nav.Route.Destination.Symbol}\n" +
                                      $"    Ship Arrival: {ship.Nav.Route.Arrival}\n" +
                                      $"    Ship Fuel: {ship.Fuel.Current} out of {ship.Fuel.Capacity}\n" +
                                      $"    Ship Cargo: {ship.Cargo.Units} out of {ship.Cargo.Capacity}"
                    );
                }

                break;
            case "orbit": 
                Console.WriteLine("Enter ship symbol:");
                string orbitSymbol = Console.ReadLine().ToUpper();
                string orbitShip = await shipService.OrbitShipAsync(orbitSymbol);
                Console.WriteLine($"Ship status: {orbitShip}");
                break;
            case "dock":
                throw new NotImplementedException();
            case "refuel":
                throw new NotImplementedException();
            case "route":
                throw new NotImplementedException();
        }
    }

    static async Task ContractsSubmenu(ContractService contractService) {
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
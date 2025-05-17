namespace Spacetraders;
using System;

class Program {
    static async Task Main(string[]? args) {
        if (args == null) {
            Console.WriteLine("Token argument required. Pass your token on launch."); // doesn't work jst need 2 catch
        }

        var httpClientService = new HttpClientService(args[0]);

        while (true) {
            Console.WriteLine("Enter a command (e.g., 'agent', 'loc') or 'exit' to quit:");
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
                    // implement new location getting
                   // Deserializer.Agent? location = await httpClientService.GetLocationAsync();
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break; 
            }
        }
    }
}
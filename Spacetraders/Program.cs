namespace Spacetraders;
using System;

class Program {
    static async Task Main(string[]? args) {
        if (args == null) {
            Console.WriteLine("Token argument required. Pass your token on launch.");
        }
        var httpClientService = new HttpClientService(args[0]);

        while (true) {
            Console.WriteLine("Enter a command (e.g., 'get-agent', 'get-loc') or 'exit' to quit:");
            var command = Console.ReadLine()?.Trim().ToLowerInvariant(); // Added Trim and ToLower for robustness

            if (command == "exit") {
                break;
            }
            
            switch (command) {
                case "get-agent":
                    Deserializer.Agent? agent = await httpClientService.GetAgentAsync();
                    if (agent != null) {
                        Console.WriteLine("Agent Details:");
                        Console.WriteLine($"  Symbol: {agent.Symbol}");
                        Console.WriteLine($"  Credits: {agent.Credits}");
                        Console.WriteLine($"  Headquarters: {agent.Headquarters}");
                        Console.WriteLine($"  Account ID: {agent.AccountId}");
                    } else {
                        Console.WriteLine("Failed to retrieve or deserialize agent data.");
                    }
                    break;
                case "get-loc":
                    string locationJson = await httpClientService.GetLocationAsync();
                    Console.WriteLine(locationJson);
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break; 
            }
        }
    }
}
using System;

class Program {
    static async Task Main(string[] args) {
        var httpClientService = new HttpClientService();

        while (true) {
            Console.WriteLine("Enter a command (e.g., 'get-agent') or 'exit' to quit:");
            var command = Console.ReadLine();

            if (command == "exit") {
                break;
            }

            Func<Task<string>> getJsonAsync = null;

            switch (command) {
                case "get-agent":
                    getJsonAsync = httpClientService.GetAgentAsync;
                    break;
                case "get-loc":
                    getJsonAsync = httpClientService.GetLocationAsync;
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    continue;
            }

            if (getJsonAsync != null) {
                var json = await getJsonAsync();
                Console.WriteLine(json);
            }
        }
    }
}

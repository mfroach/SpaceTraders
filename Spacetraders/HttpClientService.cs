using System.Net.Http.Headers;

public class HttpClientService {
    private readonly HttpClient _client;

    public HttpClientService() {
        var token = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZGVudGlmaWVyIjoiVFdJQ0VCT1VORCIsInZlcnNpb24iOiJ2Mi4zLjAiLCJyZXNldF9kYXRlIjoiMjAyNS0wMy0wMSIsImlhdCI6MTc0MTE5NjYxOSwic3ViIjoiYWdlbnQtdG9rZW4ifQ.y7nEUmnILARHYihPrhP1VrrmIvgwRMY2kVAa0IReQvtH7-MNWPwsIbMXJKONAkK7ztTnlM1HgS2v_nG29FAE5IDDe4_3YjSHTCo1vXZX8IWrel7CxqJimSzpXL1yXuq7lsvueEIbM28Dr5mHXxeejGGfYFKmBMDNr0DLqlVMP2_pJmb8wcnKzMZQvcfgj1wOR-rVV8JwKqeJv1GsWdaQr5MHhqTF1mEP4eu_PO07sREl8-Q_e4AfwXV3x-Edwl99sUTucnl9mV50uwbZEXQ_C-9lh_hPs8U0Yqcyy_kTBIS42QehEm4wuqdybnufJiLKlGylj_IdQzL5NKqlRigzbQ";
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Add("Authorization", token);
    }

    public async Task<string> GetAgentAsync() {
        var DeserializeJSON = new Deserializer();
        var json = await _client.GetStreamAsync("https://api.spacetraders.io/v2/my/agent");
        json = DeserializeJSON.DeserializeAgent(json);
        return json;
    }
    public async Task<string> GetLocationAsync() {
        var json = await _client.GetStringAsync("https://api.spacetraders.io/v2/systems/X1-J90/waypoints/X1-J90-A1");
        return json;
    }
}

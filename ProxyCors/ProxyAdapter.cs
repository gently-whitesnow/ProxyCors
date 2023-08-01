using System.Net;

namespace ProxyCors;

public class ProxyAdapter
{
    private readonly HttpClient _httpClient;

    public ProxyAdapter()
    {
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }
    public async Task<HttpResponseMessage> GetProxyResponseAsync(string requestPath)
    {
        try
        {
            return await _httpClient.GetAsync(new Uri(requestPath));
        }
        catch
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}
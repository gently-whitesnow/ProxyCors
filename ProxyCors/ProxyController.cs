using Microsoft.AspNetCore.Mvc;

namespace ProxyCors;

[Produces("application/json")]
public class ProxyController : Controller
{
    private readonly ProxyManager _proxyManager;

    public ProxyController(ProxyManager proxyManager)
    {
        _proxyManager = proxyManager;
    }
    
    [Route("/{*catchall}")]
    public Task<IActionResult> GetProxyResponseAsync()
    {
        return _proxyManager.GetProxyResponseAsync(HttpContext.Request);
    }
}
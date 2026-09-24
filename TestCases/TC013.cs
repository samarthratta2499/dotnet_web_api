namespace OrgCodeReviewCases.TC013;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public sealed class AuthAudit
{
    private readonly ILogger<AuthAudit> _logger;
    private readonly IHttpContextAccessor _http;

    public AuthAudit(ILogger<AuthAudit> logger, IHttpContextAccessor http)
    {
        _logger = logger;
        _http = http;
    }

    public void Log()
        => _logger.LogInformation("Authorization={Authorization}", _http.HttpContext?.Request.Headers.Authorization.ToString());
}

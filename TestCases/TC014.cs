namespace OrgCodeReviewCases.TC014;

using Microsoft.Extensions.Logging;

public sealed class AuthAudit
{
    private readonly ILogger<AuthAudit> _logger;

    public AuthAudit(ILogger<AuthAudit> logger) => _logger = logger;

    public void Log(string subjectId)
        => _logger.LogInformation("Authenticated subject={Subject}", subjectId);
}

namespace OrgCodeReviewCases.TC011;

using Microsoft.Extensions.Logging;

public sealed class AuditService
{
    private readonly ILogger<AuditService> _logger;

    public AuditService(ILogger<AuditService> logger) => _logger = logger;

    public void LogCitizen(string citizenId)
        => _logger.LogInformation("CitizenId={CitizenId}", citizenId);
}

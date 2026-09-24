namespace OrgCodeReviewCases.TC012;

using Microsoft.Extensions.Logging;

public static class OrgDataMasker
{
    public static string MaskCitizenId(string value)
        => value.Length <= 4 ? value : new string('*', value.Length - 4) + value[^4..];
}

public sealed class AuditService
{
    private readonly ILogger<AuditService> _logger;

    public AuditService(ILogger<AuditService> logger) => _logger = logger;

    public void LogCitizen(string citizenId)
        => _logger.LogInformation("CitizenId={CitizenId}", OrgDataMasker.MaskCitizenId(citizenId));
}

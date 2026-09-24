if (!result.IsValid)
    return BadRequest(OrgApiResponse.Fail("INVALID", "Invalid user data", traceId));

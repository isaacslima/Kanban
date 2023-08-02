namespace KanbanBackend;

public static class Helpers
{
    public static bool IsOriginAllowed(string origin)
    {
        var uri = new Uri(origin);
        var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "n/a";

        var isAllowed = uri.Host.Equals("example.com", StringComparison.OrdinalIgnoreCase);

        if (!isAllowed && env.Contains("DEV", StringComparison.OrdinalIgnoreCase))
            isAllowed = uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);

        return isAllowed;
    }
}

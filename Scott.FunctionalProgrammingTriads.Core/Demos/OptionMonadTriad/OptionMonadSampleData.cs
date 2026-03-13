namespace Scott.FunctionalProgrammingTriads.Core.Demos.OptionMonadTriad;

public static class OptionMonadSampleData
{
    private static readonly IReadOnlyDictionary<string, OptionMonadCustomer> CustomersByScenario =
        new Dictionary<string, OptionMonadCustomer>(StringComparer.OrdinalIgnoreCase)
        {
            ["complete"] = new OptionMonadCustomer
            {
                Name = "Milo",
                Profile = new OptionMonadProfile { Email = "milo@example.com" }
            },
            ["scott"] = new OptionMonadCustomer
            {
                Name = "Scott",
                Profile = new OptionMonadProfile { Email = "scott@example.com" }
            },
            ["no-profile"] = new OptionMonadCustomer
            {
                Name = "Luna",
                Profile = null
            },
            ["no-email"] = new OptionMonadCustomer
            {
                Name = "Piper",
                Profile = new OptionMonadProfile { Email = null }
            },
            ["blank-email"] = new OptionMonadCustomer
            {
                Name = "Nova",
                Profile = new OptionMonadProfile { Email = "   " }
            },
            ["bad-email"] = new OptionMonadCustomer
            {
                Name = "Kiki",
                Profile = new OptionMonadProfile { Email = "kiki-at-example.com" }
            },
            ["unknown-domain"] = new OptionMonadCustomer
            {
                Name = "Echo",
                Profile = new OptionMonadProfile { Email = "echo@unknown.dev" }
            }
        };

    private static readonly IReadOnlyDictionary<string, string> SegmentByDomain =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["example.com"] = "Internal",
            ["gmail.com"] = "Consumer",
            ["contoso.com"] = "Enterprise"
        };

    public static OptionMonadCustomer? ResolveCustomer(string? scenario)
    {
        var key = string.IsNullOrWhiteSpace(scenario) ? "complete" : scenario.Trim();
        return CustomersByScenario.TryGetValue(key, out var customer) ? customer : null;
    }

    public static string? LookupSegment(string domain) =>
        SegmentByDomain.TryGetValue(domain, out var segment)
            ? segment
            : null;
}

using ColonySimulator.Backend.Handlers;

namespace ColonySimulator.tests.Extensions;

public static class ThreatHandlerExtensions
{
    public static void SetRandomInstance(this ThreatHandler threatHandler, Random random)
    {
        var field = typeof(ThreatHandler).GetField("_random", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(threatHandler, random);
    }
}
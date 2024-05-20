using AutoFixture;
namespace Web.Integration.Tests;

public static class FixtureExtensions
{
    public static decimal CreateDecimal(this IFixture fixture, decimal min, decimal max)
    {
        return fixture.Create<decimal>() % (max - min + 1) + min;
    }
}
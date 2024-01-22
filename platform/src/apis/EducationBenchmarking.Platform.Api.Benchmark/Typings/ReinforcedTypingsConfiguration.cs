using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using EducationBenchmarking.Platform.Domain.Responses;
using Reinforced.Typings.Fluent;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace EducationBenchmarking.Platform.Api.Benchmark.Typings;

public static class ReinforcedTypingsConfiguration
{
    public static void Configure(ConfigurationBuilder builder)
    {
        builder.Global(g =>
        {
            g.CamelCaseForProperties();
            g.TabSymbol("  ");
        });

        var types = Assembly.GetAssembly(typeof(School))!
            .GetTypes()
            .Where(t => t.IsClass)
            .Where(t => t.Namespace == typeof(School).Namespace)
            .Where(t => t.Name != "<>O");

        builder
            .ExportAsInterfaces(
                types,
                c => c
                    .WithPublicProperties()
                    .DontIncludeToNamespace()
                    .AutoI(false)
            );
    }
}
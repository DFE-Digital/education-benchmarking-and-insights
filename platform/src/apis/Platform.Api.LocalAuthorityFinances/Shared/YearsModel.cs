﻿using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthorityFinances.Shared;

[ExcludeFromCodeCoverage]
public record YearsModel
{
    public int StartYear { get; set; }
    public int EndYear { get; set; }
}
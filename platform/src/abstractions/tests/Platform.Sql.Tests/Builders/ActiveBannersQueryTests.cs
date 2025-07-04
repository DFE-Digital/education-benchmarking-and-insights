﻿using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class ActiveBannersQueryTests
{
    [Fact]
    public void ShouldReturnSql()
    {
        var builder = Create();
        Assert.Equal("SELECT * FROM VW_ActiveBanners  ", builder.QueryTemplate.RawSql);
    }

    private static ActiveBannersQuery Create() => new();
}
using AutoFixture;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Models;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.StatisticalNeighbours;

public class MapperTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void MapToApiResponse_ReturnsNull_WhenCollectionIsEmpty()
    {
        var dtos = Enumerable.Empty<StatisticalNeighbourDto>();

        var result = dtos.MapToApiResponse();

        Assert.Null(result);
    }

    [Fact]
    public void MapToApiResponse_ReturnsNull_WhenLaCodeIsNull()
    {
        var dtos = new[]
        {
            new StatisticalNeighbourDto { LaCode = null }
        };

        var result = dtos.MapToApiResponse();

        Assert.Null(result);
    }

    [Fact]
    public void MapToApiResponse_ReturnsResponse_WhenCollectionIsValid()
    {
        var dtos = _fixture.CreateMany<StatisticalNeighbourDto>(3).ToArray();
        var firstDto = dtos[0];
        foreach (var dto in dtos)
        {
            dto.LaCode = firstDto.LaCode;
            dto.LaName = firstDto.LaName;
        }

        var result = dtos.MapToApiResponse();

        Assert.NotNull(result);
        Assert.Equal(firstDto.LaCode, result.Code);
        Assert.Equal(firstDto.LaName, result.Name);
        Assert.Equal(dtos.Length, result.StatisticalNeighbours!.Length);

        for (var i = 0; i < dtos.Length; i++)
        {
            Assert.Equal(dtos[i].NeighbourLaCode, result.StatisticalNeighbours[i].Code);
            Assert.Equal(dtos[i].NeighbourLaName, result.StatisticalNeighbours[i].Name);
            Assert.Equal(dtos[i].NeighbourPosition, result.StatisticalNeighbours[i].Position);
        }
    }
}

using AutoFixture;
using Web.App.Infrastructure.Apis;
using Web.App.ViewModels;
using Xunit;
namespace Web.Tests.Infrastructure.Apis.Requests;

public class GivenAPostSchoolComparatorsRequest
{
    private const string LAName = "LaName";
    private readonly Fixture _fixture;
    private readonly string _urn;

    public GivenAPostSchoolComparatorsRequest()
    {
        _fixture = new Fixture();
        _urn = _fixture.Create<string>();
    }

    [Fact]
    public void MapsTarget()
    {
        // arrange
        var viewModel = _fixture.Create<UserDefinedSchoolCharacteristicViewModel>();

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).Target;

        // assert
        Assert.Equal(_urn, actual);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("Both", new[]
    {
        "Academy",
        "Maintained"
    })]
    [InlineData("Academies", new[]
    {
        "Academy"
    })]
    [InlineData("Maintained schools", new[]
    {
        "Maintained"
    })]
    [InlineData("Out of range", null, true)]
    public void MapsFinanceTypeOrThrows(string? financeType, string[]? expected, bool exception = false)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            FinanceType = financeType
        };

        // assert
        if (exception)
        {
            Assert.Throws<ArgumentOutOfRangeException>(Action);
        }
        else
        {
            Assert.Equal(expected, Action()?.Values);
        }
        return;

        // act
        CharacteristicList? Action()
        {
            return new PostSchoolComparatorsRequest(_urn, LAName, viewModel).FinanceType;
        }
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(new[]
    {
        "Nursery"
    }, new[]
    {
        "Nursery"
    })]
    [InlineData(new[]
    {
        "Primary"
    }, new[]
    {
        "Primary"
    })]
    [InlineData(new[]
    {
        "Secondary"
    }, new[]
    {
        "Secondary"
    })]
    [InlineData(new[]
    {
        "Pupil referral units"
    }, new[]
    {
        "Pupil referral unit"
    })]
    [InlineData(new[]
    {
        "Alternative provision schools"
    }, new[]
    {
        "Alternative Provision"
    })]
    [InlineData(new[]
    {
        "Special"
    }, new[]
    {
        "Special"
    })]
    [InlineData(new[]
    {
        "University technical college"
    }, new[]
    {
        "University technical college"
    })]
    [InlineData(new[]
    {
        "All through"
    }, new[]
    {
        "All-through"
    })]
    [InlineData(new[]
    {
        "Post 16"
    }, new[]
    {
        "Post-16"
    })]
    [InlineData(new[]
    {
        "Out of range"
    }, new[]
    {
        "Out of range"
    })]
    [InlineData(new[]
    {
        "Nursery",
        "Primary",
        "Secondary"
    }, new[]
    {
        "Nursery",
        "Primary",
        "Secondary"
    })]
    public void MapsOverallPhase(string?[]? overallPhase, string[]? expected)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            OverallPhase = overallPhase
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).OverallPhase;

        // assert
        Assert.Equal(expected, actual?.Values);
    }

    [Theory]
    [InlineData("All", null, null)]
    [InlineData("Choose", new[]
    {
        "La1",
        "La2"
    }, new[]
    {
        "La1",
        "La2"
    })]
    [InlineData("This", null, new[]
    {
        LAName
    })]
    public void MapsLAName(string? laSelection, string[]? laNames, string[]? expected)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            LaSelection = laSelection,
            LaNames = laNames ?? []
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).LAName;

        // assert
        Assert.Equal(expected, actual?.Values);
    }

    [Theory]
    [InlineData("false", null, null)]
    [InlineData("true", "Include schools in deficit", "Deficit")]
    [InlineData("true", "Exclude schools in deficit", "Surplus")]
    public void MapsSchoolPosition(string? selected, string? value, string? expected)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            Deficit = selected,
            Deficits = [value!]
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).SchoolPosition;

        // assert
        Assert.Equal(expected == null ? null : [expected], actual?.Values);
    }

    [Theory]
    [InlineData("false", null, null)]
    [InlineData("true", "Part of PFI", true)]
    [InlineData("true", "Not part of PFI", false)]
    public void MapsIsPFISchool(string? selected, string? value, bool? expected)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            PrivateFinanceInitiative = selected,
            PrivateFinanceInitiatives = [value!]
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).IsPFISchool;

        // assert
        Assert.Equal(expected, actual?.Values);
    }

    [Theory]
    [InlineData("false", null, null)]
    [InlineData("true", new[]
    {
        "Inner"
    }, new[]
    {
        "Inner"
    })]
    [InlineData("true", new[]
    {
        "Outer"
    }, new[]
    {
        "Outer"
    })]
    [InlineData("true", new[]
    {
        "Neither"
    }, new[]
    {
        "Neither"
    })]
    public void MapsLondonWeighting(string? selected, string[]? values, string[]? expected)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            LondonWeighting = selected,
            LondonWeightings = values ?? []
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).LondonWeighting;

        // assert
        Assert.Equal(expected, actual?.Values);
    }

    [Theory]
    [InlineData("false", null, null)]
    [InlineData("true", new[]
    {
        "Good"
    }, new[]
    {
        "Good"
    })]
    [InlineData("true", new[]
    {
        "Requires improvement",
        "Inadequate"
    }, new[]
    {
        "Requires improvement",
        "Inadequate"
    })]
    public void MapsOfstedDescription(string? selected, string[]? values, string[]? expected)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            OfstedRating = selected,
            OfstedRatings = values ?? []
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).OfstedDescription;

        // assert
        Assert.Equal(expected, actual?.Values);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsTotalPupils(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            TotalPupils = selected,
            TotalPupilsFrom = from,
            TotalPupilsTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).TotalPupils;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsBuildingAverageAge(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            AverageBuildingAge = selected,
            AverageBuildingAgeFrom = from,
            AverageBuildingAgeTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).BuildingAverageAge;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsTotalInternalFloorArea(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            InternalFloorArea = selected,
            InternalFloorAreaFrom = from,
            InternalFloorAreaTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).TotalInternalFloorArea;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentFreeSchoolMeals(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            FreeSchoolMeals = selected,
            FreeSchoolMealsFrom = from,
            FreeSchoolMealsTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentFreeSchoolMeals;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentSpecialEducationNeeds(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            SpecialEducationalNeeds = selected,
            SpecialEducationalNeedsFrom = from,
            SpecialEducationalNeedsTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentSpecialEducationNeeds;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsTotalPupilsSixthForm(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            TotalPupilsSixthForm = selected,
            TotalPupilsSixthFormFrom = from,
            TotalPupilsSixthFormTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).TotalPupilsSixthForm;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsKS2Progress(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            KeyStage2Progress = selected,
            KeyStage2ProgressFrom = from,
            KeyStage2ProgressTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).KS2Progress;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsKS4Progress(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            KeyStage4Progress = selected,
            KeyStage4ProgressFrom = from,
            KeyStage4ProgressTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).KS4Progress;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsSchoolsInTrust(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            SchoolsInTrust = selected,
            SchoolsInTrustFrom = from,
            SchoolsInTrustTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).SchoolsInTrust;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentWithVI(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            VisualImpairment = selected,
            VisualImpairmentFrom = from,
            VisualImpairmentTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentWithVI;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentWithSPLD(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            SpecificLearningDifficulty = selected,
            SpecificLearningDifficultyFrom = from,
            SpecificLearningDifficultyTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentWithSPLD;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentWithSLD(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            SevereLearningDifficulty = selected,
            SevereLearningDifficultyFrom = from,
            SevereLearningDifficultyTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentWithSLD;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentWithSLCN(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            SpeechLanguageCommunication = selected,
            SpeechLanguageCommunicationFrom = from,
            SpeechLanguageCommunicationTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentWithSLCN;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentWithSEMH(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            SocialEmotionalMentalHealth = selected,
            SocialEmotionalMentalHealthFrom = from,
            SocialEmotionalMentalHealthTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentWithSEMH;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentWithPMLD(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            ProfoundMultipleLearningDifficulty = selected,
            ProfoundMultipleLearningDifficultyFrom = from,
            ProfoundMultipleLearningDifficultyTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentWithPMLD;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentWithPD(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            PhysicalDisability = selected,
            PhysicalDisabilityFrom = from,
            PhysicalDisabilityTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentWithPD;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentWithOTH(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            OtherLearningDifficulty = selected,
            OtherLearningDifficultyFrom = from,
            OtherLearningDifficultyTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentWithOTH;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentWithMSI(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            MultiSensoryImpairment = selected,
            MultiSensoryImpairmentFrom = from,
            MultiSensoryImpairmentTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentWithMSI;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }


    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentWithMLD(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            ModerateLearningDifficulty = selected,
            ModerateLearningDifficultyFrom = from,
            ModerateLearningDifficultyTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentWithMLD;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentWithHI(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            HearingImpairment = selected,
            HearingImpairmentFrom = from,
            HearingImpairmentTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentWithHI;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentWithASD(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedSchoolCharacteristicViewModel
        {
            AutisticSpectrumDisorder = selected,
            AutisticSpectrumDisorderFrom = from,
            AutisticSpectrumDisorderTo = to
        };

        // act
        var actual = new PostSchoolComparatorsRequest(_urn, LAName, viewModel).PercentWithASD;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }
}
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace EducationBenchmarking.Platform.Domain.Responses.Characteristics;

[ExcludeFromCodeCoverage]
public class Characteristic
{
    public string? Code { get; set; }
    public string? Description { get; set; }
}

[ExcludeFromCodeCoverage]
public static class Characteristics
{
    public static Characteristic[] All =
    {
        LaCode, NumberOfPupils, GenderOfPupils, SchoolPhase, PeriodCoveredByReturn, SchoolOverallPhase,
        TypeofEstablishment, UrbanRural, GovernmentOffice, LondonBorough, LondonWeighting,
        PercentageOfEligibleFreeSchoolMeals, PercentageOfPupilsWithStatementOfSen,
        PercentageOfPupilsOnSenRegister, PercentageOfPupilsWithEal, PercentageBoarders, AdmissionsPolicy,
        Pfi, DoesTheSchoolHave6Form, NumberIn6Form, LowestAgePupils, HighestAgePupils,
        PercentageQualifiedTeachers, FullTimeTa, FullTimeOther, FullTimeAux, SchoolWorkforceFte,
        NumberOfTeachersFte, SeniorLeadershipFte, OfstedRating, Ks2Actual, Ks2Progress,
        AverageAttainment8, Progress8Measure, SpecificLearningDifficulty, ModerateLearningDifficulty,
        SevereLearningDifficulty, ProfLearningDifficulty, SocialHealth, SpeechNeeds, HearingImpairment,
        VisualImpairment, MultiSensoryImpairment, PhysicalDisability, AutisticDisorder, OtherLearningDiff,
        PerPupilExp, PerPupilGf, RrToIncome
    };

    public static IEnumerable<string?> AllCodes => All.Select(x => x.Code);

    public static Characteristic LaCode => new() { Code = nameof(LaCode), Description = "Local Authority Code" };

    public static Characteristic NumberOfPupils =>
        new() { Code = nameof(NumberOfPupils), Description = "Number of pupils" };

    public static Characteristic GenderOfPupils =>
        new() { Code = nameof(GenderOfPupils), Description = "Gender of pupils" };

    public static Characteristic SchoolPhase => new() { Code = nameof(SchoolPhase), Description = "School phase" };

    public static Characteristic PeriodCoveredByReturn => new()
        { Code = nameof(PeriodCoveredByReturn), Description = "Period covered by return" };

    public static Characteristic SchoolOverallPhase => new()
        { Code = nameof(SchoolOverallPhase), Description = "School overall phase" };

    public static Characteristic TypeofEstablishment =>
        new() { Code = nameof(TypeofEstablishment), Description = "School type" };

    public static Characteristic UrbanRural => new() { Code = nameof(UrbanRural), Description = "Urban/rural schools" };

    public static Characteristic GovernmentOffice => new()
        { Code = nameof(GovernmentOffice), Description = "Government office region" };

    public static Characteristic LondonBorough =>
        new() { Code = nameof(LondonBorough), Description = "London borough" };

    public static Characteristic LondonWeighting =>
        new() { Code = nameof(LondonWeighting), Description = "London weighting" };

    public static Characteristic PercentageOfEligibleFreeSchoolMeals => new()
        { Code = nameof(PercentageOfEligibleFreeSchoolMeals), Description = "Eligibility for free school meals" };

    public static Characteristic PercentageOfPupilsWithStatementOfSen => new()
    {
        Code = nameof(PercentageOfPupilsWithStatementOfSen),
        Description = "Pupils with SEN who have statements or EHC plans"
    };

    public static Characteristic PercentageOfPupilsOnSenRegister => new()
    {
        Code = nameof(PercentageOfPupilsOnSenRegister),
        Description =
            "Pupils with special educational needs (SEN) who don't have statements or education and health care (EHC) plans"
    };

    public static Characteristic PercentageOfPupilsWithEal => new()
        { Code = nameof(PercentageOfPupilsWithEal), Description = "Pupils with English as an additional language" };

    public static Characteristic PercentageBoarders =>
        new() { Code = nameof(PercentageBoarders), Description = "Boarders" };

    public static Characteristic AdmissionsPolicy =>
        new() { Code = nameof(AdmissionsPolicy), Description = "Admissions policy" };

    public static Characteristic Pfi => new()
        { Code = nameof(Pfi), Description = "Part of a private finance initiative?" };

    public static Characteristic DoesTheSchoolHave6Form => new()
        { Code = nameof(DoesTheSchoolHave6Form), Description = "Does the school have a sixth form?" };

    public static Characteristic NumberIn6Form =>
        new() { Code = nameof(NumberIn6Form), Description = "Number in sixth form" };

    public static Characteristic LowestAgePupils =>
        new() { Code = nameof(LowestAgePupils), Description = "Lowest age of pupils" };

    public static Characteristic HighestAgePupils => new()
        { Code = nameof(HighestAgePupils), Description = "Highest age of pupils" };

    public static Characteristic PercentageQualifiedTeachers => new()
    {
        Code = nameof(PercentageQualifiedTeachers),
        Description = "Percentage of teachers with qualified teacher status (full time equivalent)"
    };

    public static Characteristic FullTimeTa => new()
        { Code = nameof(FullTimeTa), Description = "Number of teaching assistants (full time equivalent)" };

    public static Characteristic FullTimeOther => new()
    {
        Code = nameof(FullTimeOther),
        Description = "Number of non-classroom support staff – excluding auxiliary staff (full time equivalent)"
    };

    public static Characteristic FullTimeAux => new()
        { Code = nameof(FullTimeAux), Description = "Number of Auxiliary staff (Full Time Equivalent)" };

    public static Characteristic SchoolWorkforceFte => new()
        { Code = nameof(SchoolWorkforceFte), Description = "Number in the school workforce (full time equivalent)" };

    public static Characteristic NumberOfTeachersFte => new()
        { Code = nameof(NumberOfTeachersFte), Description = "Number of teachers (full time equivalent)" };

    public static Characteristic SeniorLeadershipFte => new()
    {
        Code = nameof(SeniorLeadershipFte), Description = "Number in the senior leadership team (full time equivalent)"
    };

    public static Characteristic OfstedRating => new() { Code = nameof(OfstedRating), Description = "Ofsted rating" };

    public static Characteristic Ks2Actual =>
        new() { Code = nameof(Ks2Actual), Description = "Key Stage 2 attainment" };

    public static Characteristic Ks2Progress =>
        new() { Code = nameof(Ks2Progress), Description = "Key Stage 2 progress" };

    public static Characteristic AverageAttainment8 => new()
        { Code = nameof(AverageAttainment8), Description = "Average Attainment 8" };

    public static Characteristic Progress8Measure =>
        new() { Code = nameof(Progress8Measure), Description = "Progress 8 measure" };

    public static Characteristic SpecificLearningDifficulty => new()
        { Code = nameof(SpecificLearningDifficulty), Description = "Specific learning difficulty" };

    public static Characteristic ModerateLearningDifficulty => new()
        { Code = nameof(ModerateLearningDifficulty), Description = "Moderate learning difficulty" };

    public static Characteristic SevereLearningDifficulty => new()
        { Code = nameof(SevereLearningDifficulty), Description = "Severe learning difficulty" };

    public static Characteristic ProfLearningDifficulty => new()
        { Code = nameof(ProfLearningDifficulty), Description = "Profound and multiple learning difficulty" };

    public static Characteristic SocialHealth => new()
        { Code = nameof(SocialHealth), Description = "Social, emotional and mental health" };

    public static Characteristic SpeechNeeds => new()
        { Code = nameof(SpeechNeeds), Description = "Speech, language and communications needs" };

    public static Characteristic HearingImpairment =>
        new() { Code = nameof(HearingImpairment), Description = "Hearing impairment" };

    public static Characteristic VisualImpairment =>
        new() { Code = nameof(VisualImpairment), Description = "Visual impairment" };

    public static Characteristic MultiSensoryImpairment => new()
        { Code = nameof(MultiSensoryImpairment), Description = "Multi-sensory impairment" };

    public static Characteristic PhysicalDisability => new()
        { Code = nameof(PhysicalDisability), Description = "Physical disability" };

    public static Characteristic AutisticDisorder => new()
        { Code = nameof(AutisticDisorder), Description = "Autistic spectrum disorder" };

    public static Characteristic OtherLearningDiff => new()
        { Code = nameof(OtherLearningDiff), Description = "Other learning difficulty" };

    public static Characteristic PerPupilExp =>
        new() { Code = nameof(PerPupilExp), Description = "Expenditure per pupil" };

    public static Characteristic PerPupilGf =>
        new() { Code = nameof(PerPupilGf), Description = "Grant funding per pupil" };

    public static Characteristic RrToIncome => new() { Code = nameof(RrToIncome), Description = "RR to Income" };
}
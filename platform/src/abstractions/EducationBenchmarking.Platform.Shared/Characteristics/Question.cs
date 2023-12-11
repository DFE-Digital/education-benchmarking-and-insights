namespace EducationBenchmarking.Platform.Shared.Characteristics;

public class Question
{
    public string? Code { get; set; }
    public string? Description { get; set; }
}

public static class Questions
{
    public static class Trusts
    {
        public static Question[] All =
        {
            NumberOfPupils, NumberOfSchools, TotalIncome, SchoolOverallPhase, CrossPrimary, CrossSecondary,
            CrossSpecial, CrossPru, CrossAp, CrossAt
        };
        
        public static IEnumerable<string?> AllCodes => All.Select(x => x.Code); 
        
        public static Question NumberOfPupils => new() { Code = nameof(NumberOfPupils), Description = "Number of pupils"};
        public static Question NumberOfSchools => new() { Code = nameof(NumberOfSchools), Description = "Number of schools" };
        public static Question TotalIncome => new() { Code = nameof(TotalIncome), Description = "Total income" };
        public static Question SchoolOverallPhase => new() { Code = nameof(SchoolOverallPhase), Description = "School phase" };
        public static Question CrossPrimary => new() { Code = nameof(CrossPrimary), Description = "Primary" };
        public static Question CrossSecondary => new() { Code = nameof(CrossSecondary), Description = "Secondary" };
        public static Question CrossSpecial => new() { Code = nameof(CrossSpecial), Description = "Special" };
        public static Question CrossPru => new() { Code = nameof(CrossPru), Description = "Pupil referral unit" };
        public static Question CrossAp => new() { Code = nameof(CrossAp), Description = "Alternative provision" };
        public static Question CrossAt => new() { Code = nameof(CrossAt), Description = "All through" };
    }
    
    
    public static class Schools
    {
        public static Question[] All = {
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

        public static Question LaCode => new() { Code = nameof(LaCode), Description = "Local Authority Code" };
        public static Question NumberOfPupils => new() { Code = nameof(NumberOfPupils), Description = "Number of pupils" };
        public static Question GenderOfPupils => new() { Code = nameof(GenderOfPupils), Description = "Gender of pupils" };
        public static Question SchoolPhase => new() { Code = nameof(SchoolPhase), Description = "School phase" };
        public static Question PeriodCoveredByReturn => new() { Code = nameof(PeriodCoveredByReturn), Description = "Period covered by return" };
        public static Question SchoolOverallPhase => new() { Code = nameof(SchoolOverallPhase), Description = "School overall phase" };
        public static Question TypeofEstablishment => new() { Code = nameof(TypeofEstablishment), Description = "School type" };
        public static Question UrbanRural => new() { Code = nameof(UrbanRural), Description = "Urban/rural schools" };
        public static Question GovernmentOffice => new() { Code = nameof(GovernmentOffice), Description = "Government office region" };
        public static Question LondonBorough => new() { Code = nameof(LondonBorough), Description = "London borough" };
        public static Question LondonWeighting => new() { Code = nameof(LondonWeighting), Description = "London weighting" };
        public static Question PercentageOfEligibleFreeSchoolMeals => new() { Code = nameof(PercentageOfEligibleFreeSchoolMeals), Description = "Eligibility for free school meals" };
        public static Question PercentageOfPupilsWithStatementOfSen => new() { Code = nameof(PercentageOfPupilsWithStatementOfSen), Description = "Pupils with SEN who have statements or EHC plans" };
        public static Question PercentageOfPupilsOnSenRegister => new() { Code = nameof(PercentageOfPupilsOnSenRegister), Description = "Pupils with special educational needs (SEN) who don't have statements or education and health care (EHC) plans" };
        public static Question PercentageOfPupilsWithEal => new() { Code = nameof(PercentageOfPupilsWithEal), Description = "Pupils with English as an additional language" };
        public static Question PercentageBoarders => new() { Code = nameof(PercentageBoarders), Description = "Boarders" };
        public static Question AdmissionsPolicy => new() { Code = nameof(AdmissionsPolicy), Description = "Admissions policy" };
        public static Question Pfi => new() { Code = nameof(Pfi), Description = "Part of a private finance initiative?" };
        public static Question DoesTheSchoolHave6Form => new() { Code = nameof(DoesTheSchoolHave6Form), Description = "Does the school have a sixth form?" };
        public static Question NumberIn6Form => new() { Code = nameof(NumberIn6Form), Description = "Number in sixth form" };
        public static Question LowestAgePupils => new() { Code = nameof(LowestAgePupils), Description = "Lowest age of pupils" };
        public static Question HighestAgePupils => new() { Code = nameof(HighestAgePupils), Description = "Highest age of pupils" };
        public static Question PercentageQualifiedTeachers => new() { Code = nameof(PercentageQualifiedTeachers), Description = "Percentage of teachers with qualified teacher status (full time equivalent)" };
        public static Question FullTimeTa => new() { Code = nameof(FullTimeTa), Description = "Number of teaching assistants (full time equivalent)" };
        public static Question FullTimeOther  => new() { Code = nameof(FullTimeOther), Description = "Number of non-classroom support staff – excluding auxiliary staff (full time equivalent)" };
        public static Question FullTimeAux => new() { Code = nameof(FullTimeAux), Description = "Number of Auxiliary staff (Full Time Equivalent)" };
        public static Question SchoolWorkforceFte => new() { Code = nameof(SchoolWorkforceFte), Description = "Number in the school workforce (full time equivalent)" };
        public static Question NumberOfTeachersFte => new() { Code = nameof(NumberOfTeachersFte), Description = "Number of teachers (full time equivalent)" };
        public static Question SeniorLeadershipFte => new() { Code = nameof(SeniorLeadershipFte), Description = "Number in the senior leadership team (full time equivalent)" };
        public static Question OfstedRating => new() { Code = nameof(OfstedRating), Description = "Ofsted rating" };
        public static Question Ks2Actual => new() { Code = nameof(Ks2Actual), Description = "Key Stage 2 attainment" };
        public static Question Ks2Progress => new() { Code = nameof(Ks2Progress), Description = "Key Stage 2 progress" };
        public static Question AverageAttainment8 => new() { Code = nameof(AverageAttainment8), Description = "Average Attainment 8" };
        public static Question Progress8Measure => new() { Code = nameof(Progress8Measure), Description = "Progress 8 measure" };
        public static Question SpecificLearningDifficulty => new() { Code = nameof(SpecificLearningDifficulty), Description = "Specific learning difficulty" };
        public static Question ModerateLearningDifficulty => new() { Code = nameof(ModerateLearningDifficulty), Description = "Moderate learning difficulty" };
        public static Question SevereLearningDifficulty => new() { Code = nameof(SevereLearningDifficulty), Description = "Severe learning difficulty" };
        public static Question ProfLearningDifficulty => new() { Code = nameof(ProfLearningDifficulty), Description = "Profound and multiple learning difficulty" };
        public static Question SocialHealth => new() { Code = nameof(SocialHealth), Description = "Social, emotional and mental health" };
        public static Question SpeechNeeds => new() { Code = nameof(SpeechNeeds), Description = "Speech, language and communications needs" };
        public static Question HearingImpairment => new() { Code = nameof(HearingImpairment), Description = "Hearing impairment" };
        public static Question VisualImpairment => new() { Code = nameof(VisualImpairment), Description = "Visual impairment" };
        public static Question MultiSensoryImpairment => new() { Code = nameof(MultiSensoryImpairment), Description = "Multi-sensory impairment" };
        public static Question PhysicalDisability => new() { Code = nameof(PhysicalDisability), Description = "Physical disability" };
        public static Question AutisticDisorder => new() { Code = nameof(AutisticDisorder), Description = "Autistic spectrum disorder" };
        public static Question OtherLearningDiff => new() { Code = nameof(OtherLearningDiff), Description = "Other learning difficulty" };
        public static Question PerPupilExp => new() { Code = nameof(PerPupilExp), Description = "Expenditure per pupil" };
        public static Question PerPupilGf => new() { Code = nameof(PerPupilGf), Description = "Grant funding per pupil" };
        public static Question RrToIncome => new() { Code = nameof(RrToIncome), Description = "RR to Income" };
    }
}
using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolViewModel
{
    private readonly School _school;
    private readonly Finances _finances;
    private readonly IEnumerable<Rating> _ratings;
    private readonly List<AreaOfInvestigation> _areaOfInvestigations;

    public SchoolViewModel(School school, Finances finances, IEnumerable<Rating> ratings)
    {
        _school = school;
        _finances = finances;
        _ratings = ratings.OrderBy(x => x.ScoreLow);
        _areaOfInvestigations = new List<AreaOfInvestigation>();
        BuildAreasOfInvestigation();
    }

    public string Name => _school.Name;
    public string Urn => _school.Urn;
    public string LastFinancialYear => $"{_finances.YearEnd}";
    public bool IsPartOfTrust => !string.IsNullOrEmpty(_school.CompanyNumber);
    public string TrustIdentifier => _school.CompanyNumber;
    public string TrustName => _school.TrustOrCompanyName;
    
    public IEnumerable<AreaOfInvestigation> AreaOfInvestigations => _areaOfInvestigations;
    
    public IEnumerable<AreaOfInvestigation> RedAreaOfInvestigations => _areaOfInvestigations.Where(x => x.RatingColour == "Red");
    public IEnumerable<AreaOfInvestigation> AmberAreaOfInvestigations => _areaOfInvestigations.Where(x => x.RatingColour == "Amber");
    public IEnumerable<AreaOfInvestigation> GreenAreaOfInvestigations => _areaOfInvestigations.Where(x => x.RatingColour != "Amber" && x.RatingColour != "Red");

    private void BuildAreasOfInvestigation()
    {
        AddAreaOfInvestigationWithRating("Spending","Teaching staff",_finances.TeachingStaffCosts / _finances.TotalExpenditure);
        AddAreaOfInvestigationWithRating("Spending","Supply staff", _finances.SupplyTeachingStaffCosts/ _finances.TotalExpenditure);
        AddAreaOfInvestigationWithRating("Spending","Education support staff", _finances.EducationSupportStaffCosts/_finances.TotalExpenditure);
        AddAreaOfInvestigationWithRating("Spending","Administrative and clerical staff", _finances.AdministrativeClericalStaffCosts/_finances.TotalExpenditure);
        AddAreaOfInvestigationWithRating("Spending","Other staff costs", _finances.OtherStaffCosts / _finances.TotalExpenditure);
        AddAreaOfInvestigationWithRating("Spending","Premises costs", _finances.MaintenancePremisesCosts /_finances.TotalExpenditure);
        AddAreaOfInvestigationWithRating( "Spending","Educational supplies",_finances.BreakdownEducationalSuppliesCosts/_finances.TotalExpenditure);
        //AddAreaOfInvestigationWithRating("Spending", "Energy", _finances.Energy/_finances.TotalExpenditure);
        //AddAreaOfInvestigationWithRating("Reserve and balance", "In-year balance",_finances.InYearBalance/_finances.TotalIncome);
        //AddAreaOfInvestigationWithRating("Reserve and balance", "Revenue reserve",_finances.RevenueReserve/_finances.TotalIncome);
        _areaOfInvestigations.Add(new AreaOfInvestigation { AreaGroup = "School characteristics", AreaName = "Average teacher cost"} );//, null, null);
        _areaOfInvestigations.Add(new AreaOfInvestigation { AreaGroup = "School characteristics", AreaName = "Senior leaders as a percentage of workforce"} );//, null, null);
        _areaOfInvestigations.Add(new AreaOfInvestigation { AreaGroup = "School characteristics", AreaName = "Pupil to teacher ratio"} );//, null, null);
        _areaOfInvestigations.Add(new AreaOfInvestigation { AreaGroup = "School characteristics", AreaName = "Pupil to adult ratio"} );//, null, null);
        _areaOfInvestigations.Add(new AreaOfInvestigation { AreaGroup = "School characteristics", AreaName = "Teacher contact ratio (less than 1)"} );//, null, null);
        _areaOfInvestigations.Add(new AreaOfInvestigation { AreaGroup = "School characteristics", AreaName = "Predicted percentage pupil number change in 3-5 years"} );//,null, null);
        _areaOfInvestigations.Add(new AreaOfInvestigation { AreaGroup = "School characteristics", AreaName = "Average Class size"} );//, null, null);
    }

    private void AddAreaOfInvestigationWithRating(string group, string area, decimal score)
    {
        score = Math.Round(score, 3);
        
        var ratings = _ratings.Where(x => x.AssessmentArea == area);
        var rating = ratings.FirstOrDefault(x => x.ScoreLow <= score && x.ScoreHigh >= score);
        
        _areaOfInvestigations.Add(new AreaOfInvestigation
        {
            AreaGroup = group, 
            AreaName = area, 
            Score =  score,
            RatingText = rating?.RatingText,
            RatingColour = rating?.RatingColour
        });
    }
}
namespace EducationBenchmarking.Web.Domain;

    public record Finances
    {
        public string URN { get; set; }
        public string SchoolName { get; set; }
        public int YearEnd { get; set; }
    }
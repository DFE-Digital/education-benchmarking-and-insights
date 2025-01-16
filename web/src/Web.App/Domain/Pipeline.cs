namespace Web.App.Domain;

public static class Pipeline
{
    public static class RunType
    {
        public const string Default = "default";
        public const string Custom = "custom";
    }

    public static class JobType
    {
        public const string Default = "default";
        public const string ComparatorSet = "comparator-set";
        public const string CustomData = "custom-data";
    }

    public static class JobStatus
    {
        public const string Complete = "complete";
        public const string Pending = "pending";
        public const string Removed = "removed";
    }
}
namespace Platform.Domain;

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
}
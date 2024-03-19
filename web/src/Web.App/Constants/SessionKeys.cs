namespace Web.App;

public static class SessionKeys
{
    public static string DefaultPupilComparatorSet(string urn) => $"default-pupil-comparator-set-{urn}";

    public static string DefaultAreaComparatorSet(string urn) => $"default-area-comparator-set-{urn}";
}
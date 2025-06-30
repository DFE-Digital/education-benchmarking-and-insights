namespace Web.App.Infrastructure.Apis.Content;

public static class Api
{
    public static class Banners
    {
        public static string Banner(string target)
        {
            return $"api/banner/{target}";
        }
    }

    public static class CommercialResources
    {
        public static string Resources => "api/commercial-resources";
    }

    public static class Files
    {
        public static string Transparency => "api/files/transparency";
    }

    public static class Years
    {
        public static string CurrentReturnYears => "api/current-return-years";
    }
}
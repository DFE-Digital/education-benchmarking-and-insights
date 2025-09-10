namespace Web.App.Infrastructure.Apis.Content;

public static class Api
{
    public static class Banners
    {
        public static string Banner(string target) => $"api/banner/{target}";
    }

    public static class CommercialResources
    {
        public static string Resources => "api/commercial-resources";
    }

    public static class Files
    {
        public static string Transparency => "api/files/transparency";
    }

    public static class News
    {
        public static string All => "api/news";

        public static string Article(string slug) => $"api/news/{slug}";
    }

    public static class Years
    {
        public static string CurrentReturnYears => "api/current-return-years";
    }
}
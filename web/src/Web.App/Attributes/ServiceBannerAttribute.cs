namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ServiceBannerAttribute(string target) : Attribute
{
    public string Target => target;
}
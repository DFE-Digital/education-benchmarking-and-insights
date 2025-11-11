namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PropertyOrderAttribute(int order) : Attribute
{
    public int Order => order;
}
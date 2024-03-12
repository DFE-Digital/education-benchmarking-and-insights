using System.Diagnostics.CodeAnalysis;

namespace Web.App.Attributes
{
    [ExcludeFromCodeCoverage]
    public class StringValueAttribute(string value) : Attribute
    {
        public string StringValue { get; protected set; } = value;
    }
}
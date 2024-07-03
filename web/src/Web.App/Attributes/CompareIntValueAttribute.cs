namespace Web.App.Attributes;

public class CompareIntValueAttribute(string otherProperty, Operator operatorType, string errorFormatString = "{0} {1} {2}")
    : CompareValueAttribute<int>(otherProperty, operatorType, errorFormatString)
{
    protected override bool GreaterThan(int propertyValue, int otherPropertyValue) => propertyValue > otherPropertyValue;
    protected override bool GreaterThanOrEqualTo(int propertyValue, int otherPropertyValue) => propertyValue >= otherPropertyValue;
    protected override bool LessThan(int propertyValue, int otherPropertyValue) => propertyValue < otherPropertyValue;
    protected override bool LessThanOrEqualTo(int propertyValue, int otherPropertyValue) => propertyValue <= otherPropertyValue;
}
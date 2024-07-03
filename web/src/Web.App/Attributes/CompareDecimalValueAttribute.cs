namespace Web.App.Attributes;

public class CompareDecimalValueAttribute(string otherProperty, Operator operatorType, string errorFormatString = "{0} {1} {2}")
    : CompareValueAttribute<decimal>(otherProperty, operatorType, errorFormatString)
{
    protected override bool GreaterThan(decimal propertyValue, decimal otherPropertyValue) => propertyValue > otherPropertyValue;
    protected override bool GreaterThanOrEqualTo(decimal propertyValue, decimal otherPropertyValue) => propertyValue >= otherPropertyValue;
    protected override bool LessThan(decimal propertyValue, decimal otherPropertyValue) => propertyValue < otherPropertyValue;
    protected override bool LessThanOrEqualTo(decimal propertyValue, decimal otherPropertyValue) => propertyValue <= otherPropertyValue;
}
namespace Web.App.Attributes;

public class CompareDecimalValueAttribute(string otherProperty, Operator operatorType) : CompareValueAttribute<decimal>(otherProperty, operatorType)
{
    protected override bool GreaterThan(decimal propertyValue, decimal otherPropertyValue) => propertyValue > otherPropertyValue;
    protected override bool GreaterThanOrEqualTo(decimal propertyValue, decimal otherPropertyValue) => propertyValue >= otherPropertyValue;
    protected override bool LessThan(decimal propertyValue, decimal otherPropertyValue) => propertyValue < otherPropertyValue;
    protected override bool LessThanOrEqualTo(decimal propertyValue, decimal otherPropertyValue) => propertyValue <= otherPropertyValue;
}
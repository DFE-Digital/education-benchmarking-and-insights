using System.Security.Claims;

namespace EducationBenchmarking.Web.Identity.Models;


public class OperationClaim : Claim, IEquatable<OperationClaim>
{
    public OperationClaim(string name, string context, string text) : base(ClaimNames.Operation, name)
    {
        Name = name;
        Context = context;
        Text = text;
    }

    public string Name { get; }
    public string Context { get; }
    public string Text { get; }

    public static implicit operator OperationClaim(string op)
    {
        return new OperationClaim(op, "", "");
    }
        
    public static implicit operator string(OperationClaim op)
    {
        return op.Name;
    }

    public bool Equals(OperationClaim other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return String.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((OperationClaim) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ((Name != null ? Name.GetHashCode() : 0) * 397);
        }
    }

    public void Deconstruct(out string value)
    {
        value = Name;
    }
}
namespace InheritanceIssueDemo.Domain.Common.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public bool Equals(ValueObject? other) => Equals(obj: other);

    public abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        ValueObject valueObject = (ValueObject)obj;

        return GetEqualityComponents()
            .SequenceEqual(second: valueObject.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject left, ValueObject right) => Equals(left, right);

    public static bool operator !=(ValueObject left, ValueObject right) => !Equals(left, right);

    public override int GetHashCode()
    {
        return GetEqualityComponents()
               .Select(x => x?.GetHashCode() ?? 0)
               .Aggregate((x, y) => x ^ y);
    }
}

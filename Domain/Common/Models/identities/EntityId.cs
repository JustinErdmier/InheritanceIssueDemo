namespace InheritanceIssueDemo.Domain.Common.Models.identities;

public abstract class EntityId<TId> : ValueObject
{
    protected EntityId(TId value) => Value = value;

#pragma warning disable CS8618
    protected EntityId()
#pragma warning restore CS8618
    { }

    public TId Value { get; }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string? ToString() => Value?.ToString() ?? base.ToString();
}

namespace InheritanceIssueDemo.Domain.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : ValueObject
{
    protected Entity(TId id) => Id = id;

#pragma warning disable CS8618
    protected Entity()
    { }
#pragma warning restore CS8618

    public TId Id { get; protected init; }

    public bool Equals(Entity<TId>? other) => Equals(obj: other);

    public override bool Equals(object? obj) => obj is Entity<TId> entity && Id.Equals(entity.Id);

    public static bool operator ==(Entity<TId> left, Entity<TId> right) => Equals(left, right);

    public static bool operator !=(Entity<TId> left, Entity<TId> right) => !Equals(left, right);

    public override int GetHashCode() => Id.GetHashCode();
}

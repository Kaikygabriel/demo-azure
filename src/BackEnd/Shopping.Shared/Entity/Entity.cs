namespace Shopping.Shared.Entity;

public abstract class Entity
{
    public Guid Id { get; private init; } = Guid.NewGuid();
}
namespace Shopping.Domain.BackOffice.Abstraction;

public abstract class Entity
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
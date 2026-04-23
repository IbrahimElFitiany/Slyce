namespace Shared.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; protected init; }
        public DateTime CreatedAt{ get; protected init; }
        public DateTime UpdatedAt { get; protected set; }

    }
}
namespace Domain.Abstractions
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; protected set; }

        private readonly List<IDomainEvent> _events = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _events;

        protected virtual void Apply(IDomainEvent @event)
        {
            When(@event);
            _events.Add(@event);
        }

        protected abstract void When(IDomainEvent @event);
    }
}
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using System.Reflection;

namespace Infrastructure.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<Guid, List<IDomainEvent>> _store = new();

        public Task SaveAsync(AggregateRoot aggregate)
        {
            if (!_store.ContainsKey(aggregate.Id))
                _store[aggregate.Id] = new();

            _store[aggregate.Id].AddRange(aggregate.DomainEvents);
            return Task.CompletedTask;
        }


        public async Task<AggregateRoot> LoadAsync(Guid id,
        Func<AggregateRoot> aggregateFactory)
        {
                var aggregate = aggregateFactory();

                if (!_store.ContainsKey(id))
                    return aggregate;

                foreach (var @event in _store[id])
                {
                    aggregate.GetType()
                        .GetMethod("When", BindingFlags.NonPublic | BindingFlags.Instance)!
                        .Invoke(aggregate, new object[] { @event });
                }

                return aggregate;
        }
    }
}

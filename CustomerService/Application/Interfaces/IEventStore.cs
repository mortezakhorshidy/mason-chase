using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEventStore
    {
        Task SaveAsync(AggregateRoot aggregate);

        Task<AggregateRoot> LoadAsync(
            Guid id,
            Func<AggregateRoot> aggregateFactory);
    }
}

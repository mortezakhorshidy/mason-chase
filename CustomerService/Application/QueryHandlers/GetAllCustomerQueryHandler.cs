using Application.Commands;
using Application.Interfaces;
using Domain.Abstractions;
using Domain.Aggregates;
using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class GetAllCustomerQueryHandler
    {
        private readonly IEventStore _eventStore;

        public GetAllCustomerQueryHandler(
            IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<IReadOnlyList<AggregateRoot>> Handle()
        {
            var customers = await _eventStore.LoadAllAsync(() => (Customer)Activator.CreateInstance(
        typeof(Customer),
        nonPublic: true)!);
            return customers;
        }
    }
}

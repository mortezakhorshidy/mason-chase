using Application.Commands;
using Application.Interfaces;
using Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class DeleteCustomerCommandHandler
    {
        private readonly IEventStore _eventStore;

        public DeleteCustomerCommandHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task Handle(DeleteCustomerCommand command)
        {
            var customer = (Customer)await _eventStore.LoadAsync(
                command.CustomerId,
                () => (Customer)Activator.CreateInstance(
                    typeof(Customer),
                    nonPublic: true)!);

            customer.Delete();
            await _eventStore.SaveAsync(customer);
        }
    }
}

using Application.Commands;
using Application.Interfaces;
using Domain.Aggregates;
using Domain.Exceptions;
using Domain.ValueObjects;


namespace Application.CommandHandlers
{
    public class UpdateCustomerCommandHandler
    {
        private readonly IEventStore _eventStore;
        private readonly ICustomerUniquenessChecker _uniquenessChecker;

        public UpdateCustomerCommandHandler(
            IEventStore eventStore,
            ICustomerUniquenessChecker uniquenessChecker)
        {
            _eventStore = eventStore;
            _uniquenessChecker = uniquenessChecker;
        }

        public async Task Handle(UpdateCustomerCommand command)
        {
            if (!await _uniquenessChecker
                    .IsEmailUnique(command.Email, command.CustomerId))
                throw new DomainException("Email already exists");

            var customer = (Customer)await _eventStore.LoadAsync(
                  command.CustomerId,
                  () => (Customer)Activator.CreateInstance(
                      typeof(Customer),
                      nonPublic: true)!);

            customer.Update(
                command.FirstName,
                command.LastName,
                command.DateOfBirth,
                new Email(command.Email),
                new PhoneNumber(command.PhoneNumber),
                new BankAccountNumber(command.BankAccountNumber));

            await _eventStore.SaveAsync(customer);
        }
    }
}

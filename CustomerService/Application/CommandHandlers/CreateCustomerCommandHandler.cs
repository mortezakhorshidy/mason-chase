using Application.Commands;
using Application.Interfaces;
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
    public class CreateCustomerCommandHandler
    {
        private readonly IEventStore _eventStore;
        private readonly ICustomerUniquenessChecker _uniquenessChecker;

        public CreateCustomerCommandHandler(
            IEventStore eventStore,
            ICustomerUniquenessChecker uniquenessChecker)
        {
            _eventStore = eventStore;
            _uniquenessChecker = uniquenessChecker;
        }

        public async Task<Customer> Handle(CreateCustomerCommand command)
        {
            if (!await _uniquenessChecker.IsCustomerUnique(
                    command.FirstName,
                    command.LastName,
                    command.DateOfBirth))
                throw new DomainException("Customer already exists");

            if (!await _uniquenessChecker.IsEmailUnique(command.Email))
                throw new DomainException("Email already exists");

            var customer = Customer.Create(
                command.FirstName,
                command.LastName,
                command.DateOfBirth,
                new Email(command.Email),
                new PhoneNumber(command.PhoneNumber),
                new BankAccountNumber(command.BankAccountNumber));

            await _eventStore.SaveAsync(customer);


            return customer;
        }
    }
}

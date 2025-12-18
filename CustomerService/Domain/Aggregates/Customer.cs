using Domain.Abstractions;
using Domain.Events;
using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Domain.Aggregates
{
    public class Customer : AggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public bool IsDeleted { get; private set; }

        public void ClearDomainEvents() => _domainEvents.Clear();

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        private Customer() { }

        public static Customer Create(
            string firstName,
            string lastName,
            DateTime dob,
            Email email,
            PhoneNumber phoneNumber,
            BankAccountNumber bankAccountNumber)
        {
            var customer = new Customer();

            var @event = new CustomerCreatedEvent(
                Guid.NewGuid(),
                firstName,
                lastName,
                dob,
                email.Value,
                phoneNumber.Value,
                bankAccountNumber.Value);

            customer.AddDomainEvent(@event);  
            customer.Apply(@event);           

            return customer;
        }

        public void Update(
            string firstName,
            string lastName,
            DateTime dob,
            Email email,
            PhoneNumber phone,
            BankAccountNumber bank)
        {
            if (IsDeleted)
                throw new DomainException("Customer is deleted");

            var @event = new CustomerUpdatedEvent(
                Id,
                firstName,
                lastName,
                dob,
                email.Value,
                phone.Value,
                bank.Value);

            AddDomainEvent(@event);
            Apply(@event);
        }

        public void Delete()
        {
            if (IsDeleted) return;

            var @event = new CustomerDeletedEvent(Id);
            AddDomainEvent(@event);
            Apply(@event);
        }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case CustomerCreatedEvent e:
                    Id = e.CustomerId;
                    IsDeleted = false;
                    break;

                case CustomerUpdatedEvent e:
                    break;

                case CustomerDeletedEvent:
                    IsDeleted = true;
                    break;
            }
        }

        protected override void Apply(IDomainEvent @event)
        {
            When(@event);
        }
    }
}
using Domain.Aggregates;
using Domain.Events;
using Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Domain.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void CreateCustomer_WithValidData_ShouldRaiseCustomerCreatedEvent()
        {
            var firstName = "morteza";
            var lastName = "khorshidy";
            var dob = new DateTime(1990, 1, 1);
            PhoneNumber phone = new PhoneNumber("+989153099703");
            Email email = new Email("morteza.khorshidy@gmail.com");
            BankAccountNumber bank = new BankAccountNumber("1234567890123456");

            var customer = Customer.Create(
                firstName,
                lastName,
                dob,
                email,
                phone,
                bank
            );

            customer.DomainEvents
                .Should()
                .ContainSingle(e => e is CustomerCreatedEvent);
        }
    }

}
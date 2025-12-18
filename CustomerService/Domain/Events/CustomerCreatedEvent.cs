using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public record CustomerCreatedEvent(
        Guid CustomerId,
        string FirstName,
        string LastName,
        DateTime DateOfBirth,
        string Email,
        string PhoneNumber,
        string BankAccountNumber
    ) : IDomainEvent;
}

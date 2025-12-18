using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public record UpdateCustomerCommand(
        Guid CustomerId,
        string FirstName,
        string LastName,
        DateTime DateOfBirth,
        string Email,
        string PhoneNumber,
        string BankAccountNumber
    );
}

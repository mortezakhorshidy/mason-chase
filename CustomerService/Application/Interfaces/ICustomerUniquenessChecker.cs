using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerUniquenessChecker
    {
        Task<bool> IsCustomerUnique(
            string firstName,
            string lastName,
            DateTime dateOfBirth);

        //Task<bool> IsEmailUnique(string email);

        Task<bool> IsEmailUnique(string email, Guid? excludingCustomerId = null);
    }
}

using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InMemoryCustomerUniquenessChecker : ICustomerUniquenessChecker
    {
        private readonly List<CustomerRecord> _customers = new();

        private record CustomerRecord(
            Guid Id,
            string FirstName,
            string LastName,
            DateTime DateOfBirth,
            string Email
        );

        public Task<bool> IsCustomerUnique(
            string firstName,
            string lastName,
            DateTime dateOfBirth)
        {
            var exists = _customers.Any(c =>
                c.FirstName == firstName &&
                c.LastName == lastName &&
                c.DateOfBirth == dateOfBirth);

            return Task.FromResult(!exists);
        }

        public Task<bool> IsEmailUnique(
            string email,
            Guid? excludingCustomerId = null)
        {
            var exists = _customers.Any(c =>
                c.Email == email &&
                (!excludingCustomerId.HasValue || c.Id != excludingCustomerId));

            return Task.FromResult(!exists);
        }

        public void Add(Guid id, string firstName, string lastName, DateTime dob, string email)
        {
            _customers.Add(new CustomerRecord(id, firstName, lastName, dob, email));
        }

        public void UpdateEmail(Guid id, string email)
        {
            var customer = _customers.Single(c => c.Id == id);
            _customers.Remove(customer);
            _customers.Add(customer with { Email = email });
        }

        public void Remove(Guid id)
        {
            _customers.RemoveAll(c => c.Id == id);
        }
    }
}

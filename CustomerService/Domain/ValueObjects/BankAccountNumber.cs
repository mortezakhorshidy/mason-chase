using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class BankAccountNumber
    {
        public string Value { get; }

        public BankAccountNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 10 || !value.All(char.IsDigit))
                throw new DomainException("Bank account number is invalid");

            Value = value;
        }
    }
}

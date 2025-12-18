using Domain.Exceptions;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class PhoneNumber
    {
        public string Value { get; }

        public PhoneNumber(string number)
        {
            if (!IsValidMobile(number))
                throw new DomainException("Phone number is not a valid mobile number");

            Value = number;
        }

        private static bool IsValidMobile(string number)
        {
            var util = PhoneNumberUtil.GetInstance();

            try
            {
                var parsed = util.Parse(number, "IR"); 
                return util.IsValidNumber(parsed)
                       && util.GetNumberType(parsed) == PhoneNumberType.MOBILE;
            }
            catch
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Model
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        public ValidEmailDomainAttribute(string allowedDomain)
        {
            this.AllowedDomain = allowedDomain;
        }

        public string AllowedDomain { get; }

        public override bool IsValid(object value)
        {
            string[] strings = value.ToString().Split('@');
            return strings[1].ToUpper() == AllowedDomain.ToUpper();

        }
    }
}

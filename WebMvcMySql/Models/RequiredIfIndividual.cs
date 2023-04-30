using ExpressiveAnnotations.Attributes;
using System;
using System.ComponentModel.DataAnnotations;


namespace WebMvcMySql.Models
{
    public class RequiredIfIndividualAttribute : RequiredIfAttribute
    {
        public RequiredIfIndividualAttribute(string dependentProperty) : base(dependentProperty + " && TypePerson == 0")
        {
            DependentProperty = dependentProperty;
        }
    }
    }
}

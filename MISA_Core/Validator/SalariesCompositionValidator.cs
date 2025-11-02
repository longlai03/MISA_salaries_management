using FluentValidation;
using MISA_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Validator
{
    public class SalariesCompositionValidator: AbstractValidator<SalariesComposition>
    {
        public SalariesCompositionValidator()
        {
        }
    }
}

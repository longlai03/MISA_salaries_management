using MISA_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Validator
{
    public class SalariesCompositionSystemValidator
    {
        public Dictionary<string, string> Validate(SalariesCompositionSystem entity)
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(entity.ComponentCode))
                errors.Add(nameof(entity.ComponentCode), "Mã thành phần lương không được để trống.");

            if (string.IsNullOrWhiteSpace(entity.ComponentName))
                errors.Add(nameof(entity.ComponentName), "Tên thành phần lương không được để trống.");

            if (entity.ComponentType == null)
                errors.Add(nameof(entity.ComponentType), "Loại thành phần lương không được để trống.");

            return errors;
        }
    }
}

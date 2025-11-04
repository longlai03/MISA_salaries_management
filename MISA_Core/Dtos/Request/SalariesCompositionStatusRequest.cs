using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Dtos.Request
{
    public class SalariesCompositionStatusRequest
    {
        public required List<Guid> SalaryCompositionIds { get; set; }
        public bool Status { get; set; }
    }
}

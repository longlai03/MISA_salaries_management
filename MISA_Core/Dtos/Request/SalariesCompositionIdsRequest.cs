using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Dtos.Request
{
    public class SalariesCompositionIdsRequest
    {
        public required List<Guid> SalaryCompositionIds { get; set; }
    }
}

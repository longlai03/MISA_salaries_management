using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Dtos.Request
{
    public class SalariesCompositionSystemIdsRequest
    {
        public required List<Guid> SalaryCompositionSystemIds { get; set; }

    }
}

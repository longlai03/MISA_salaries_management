using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Dtos.Response
{
    public class PageDataResponse<T>
    {
        public IEnumerable<T>? CustomData { get; set; }
        public IEnumerable<T>? PageData { get; set; }
        public object? SummaryData { get; set; }
        public int Total { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Entities
{
    public class SalariesComposition
    {
        public Guid? SalariesCompositionId { get; set; }
        public string? ComponentCode { get; set; }
        public string? ComponentnName { get; set; }
        public string? OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
        public string? ComponentType { get; set; }
        public int? Nature { get; set; }
        public int? TaxMode { get; set; }
        public bool? IsTaxDeductible { get; set; }
        public string? Quota { get; set; }
        public bool? AllowOverQuota { get; set; }
        public string? ValueType { get; set; }
        public string? Value { get; set; }
        public string? ValueCalcScope { get; set; }
        public int? ValueCalcSumEmployeeFormula { get; set; }
        public string? ValueCalcManualFormula { get; set; }
        public string? ValueTaxablePortion { get; set; }
        public string? ValueTaxFreePortion { get; set; }
        public string? Description {  get; set; }
        public bool? ShowOnSalary { get; set; }
        public int? CreateSource { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}

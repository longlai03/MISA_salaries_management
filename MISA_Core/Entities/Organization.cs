using MISA.Core.MISAAttribute;
using MISA_Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Entities
{
    /// <summary>
    /// Bảng đơn vị công tác (organization)
    /// </summary>
    [MISATableName("pa_organization")]
    public class Organization
    {
        //PK
        [MISAColumnName("organization_id")]
        public Guid? OrganizationId { get; set; }
        //Tên đơn vị công tác
        [MISAColumnName("organization_name")]
        public string? OrganizationName { get; set; }
        //Id đơn vị công tác cha
        [MISAColumnName("organization_parent_id")]
        public Guid? OrganizationParentId { get; set; }
    }
}

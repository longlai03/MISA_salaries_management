using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.MISAAttribute
{
    /// <summary>
    /// Attribute dùng để định nghĩa tên cột trong cơ sở dữ liệu tương ứng với thuộc tính của entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAColumnNameAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public MISAColumnNameAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}

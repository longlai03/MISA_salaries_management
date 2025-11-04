using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.MISAAttribute
{
    /// <summary>
    /// Attribute dùng để định nghĩa tên bảng trong cơ sở dữ liệu tương ứng với lớp entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MISATableNameAttribute : Attribute
    {
        public string TableName { get; set; }
        public MISATableNameAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}

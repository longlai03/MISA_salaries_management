using MISA.Core.MISAAttribute;
using MISA_Core.MISAAttribute;

namespace MISA_Core.Entities
{
    /// <summary>
    /// Bảng thành phần lương hệ thống
    /// </summary>
    [MISATableName("pa_salary_composition_system")]
    public class SalariesCompositionSystem
    {
        //PK
        [MISAColumnName("salary_composition_system_id")]
        public Guid SalaryCompositionSystemId { get; set; }

        //Mã thành phần lương
        [MISAColumnName("component_code")]
        public string? ComponentCode { get; set; }

        //Tên thành phần lương
        [MISAColumnName("component_name")]
        public string? ComponentName { get; set; }

        //Id đơn vị tổ chức
        [MISAColumnName("organization_id")]
        public string? OrganizationId { get; set; }

        //Tên đơn vị tổ chức
        [MISAColumnName("organization_name")]
        public string? OrganizationName { get; set; }

        //Loại thành phần lương
        [MISAColumnName("component_type")]
        public int? ComponentType { get; set; }

        //Tính chất
        [MISAColumnName("nature")]
        public int? Nature { get; set; }

        //Chế độ thuế
        [MISAColumnName("tax_mode")]
        public int? TaxMode { get; set; }

        //Có được trừ thuế không
        [MISAColumnName("is_tax_deductible")]
        public bool? IsTaxDeductible { get; set; }

        //Hạn mức
        [MISAColumnName("quota")]
        public string? Quota { get; set; }

        //Có được vượt hạn mức không
        [MISAColumnName("allow_over_quota")]
        public bool? AllowOverQuota { get; set; }

        //Kiểu giá trị
        [MISAColumnName("value_type")]
        public int? ValueType { get; set; }

        //Giá trị
        [MISAColumnName("value")]
        public int? Value { get; set; }

        //Phạm vi tính toán
        [MISAColumnName("value_calc_scope")]
        public int? ValueCalcScope { get; set; }

        //Tổng giá trị của các nhân vên
        [MISAColumnName("value_calc_sum_employee_formula")]
        public int? ValueCalcSumEmployeeFormula { get; set; }

        //Công thức tự đặt
        [MISAColumnName("value_calc_manual_formula")]
        public string? ValueCalcManualFormula { get; set; }

        //Phần chịu thuế
        [MISAColumnName("value_taxable_portion")]
        public string? ValueTaxablePortion { get; set; }

        //Phần miễn thuế
        [MISAColumnName("value_tax_free_portion")]
        public string? ValueTaxFreePortion { get; set; }

        //Mô tả
        [MISAColumnName("description")]
        public string? Description { get; set; }

        //Hiển thị trên bảng lương
        [MISAColumnName("show_on_salary")]
        public int? ShowOnSalary { get; set; }

        //Nguồn tạo
        [MISAColumnName("create_source")]
        public int CreateSource { get; set; }

        //Trạng thái
        [MISAColumnName("status")]
        public int Status { get; set; }

        //Ngày tạo
        [MISAColumnName("create_at")]
        public DateTime? CreateAt { get; set; }

        //Ngày cập nhật
        [MISAColumnName("update_at")]
        public DateTime? UpdateAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MISA_Core.Dto.Response
{
    /// <summary>
    /// Lớp phản hồi chuẩn cho tất cả các API
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của nội dung trả về</typeparam>
    public class APIResponse<T> where T : class
    {
        public APIResponse()
        {
            ServerTime = DateTime.Now;
        }

        // Trạng thái thành công
        [JsonPropertyName("success")]
        public bool Success { get; set; } = true;

        // Phản hồi người dùng
        [JsonPropertyName("userMessage")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string UserMessage { get; set; }

        // Phản hồi hệ thống
        [JsonPropertyName("systemMessage")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SystemMessage { get; set; }

        // Thông tin validate
        [JsonPropertyName("validateInfo")]
        public IEnumerable<object> ValidateInfo { get; set; } = new List<object>();

        // Trạng thái lấy dữ liệu mới nhất
        [JsonPropertyName("getLastData")]
        public bool GetLastData { get; set; } = true;

        // Thời gian của server
        [JsonPropertyName("serverTime")]
        public DateTime ServerTime { get; set; }

        // dữ liệu
        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data { get; set; }
    }
}

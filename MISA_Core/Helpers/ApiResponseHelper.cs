using MISA_Core.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Helpers
{
    public class ApiResponseHelper
    {
        /// <summary>
        /// Tạo phản hồi thành công cho API
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của nội dung phản hồi</typeparam>
        /// <param name="data">Dữ liệu trả về (có thể null)</param>
        /// <param name="userMessage">Thông báo người dùng</param>
        /// <param name="httpStatus">Mã trạng thái HTTP (mặc định 200)</param>
        /// <returns>Đối tượng phản hồi API thành công</returns>
        public static APIResponse<T> Success<T>(T? data = null, string? userMessage = "Thành công") where T : class
        {
            return new APIResponse<T>
            {
                Success = true,
                UserMessage = userMessage,
                ServerTime = DateTime.Now,
                ValidateInfo = new List<object>(),
                Data = data
            };
        }

        /// <summary>
        /// Tạo phản hồi lỗi cho API
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của nội dung phản hồi</typeparam>
        /// <param name="userMessage">Thông báo lỗi hiển thị cho người dùng</param>
        /// <param name="systemMessage">Thông báo chi tiết cho hệ thống (tùy chọn)</param>
        /// <param name="httpStatus">Mã trạng thái HTTP (mặc định 400)</param>
        /// <returns>Đối tượng phản hồi API lỗi</returns>
        public static APIResponse<T> Error<T>(
            string userMessage,
            string? systemMessage = null) where T : class
        {
            return new APIResponse<T>
            {
                Success = false,
                UserMessage = userMessage,
                SystemMessage = systemMessage,
                ValidateInfo = new List<object>(),
                ServerTime = DateTime.UtcNow
            };
        }
    }
}

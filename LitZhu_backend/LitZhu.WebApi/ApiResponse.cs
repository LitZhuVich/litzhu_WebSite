namespace LitZhu.WebApi
{
    public class ApiResponse
    {
        /// <summary>
        /// 返回的状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponse Success(object? data = null)
        {
            return new ApiResponse
            {
                Code = 200,
                Message = "success",
                Data = data ?? ""
            };
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponse Fail(object? data = null)
        {
            return new ApiResponse
            {
                Code = 500,
                Message = "error",
                Data = data ?? ""
            };
        }
    }
}

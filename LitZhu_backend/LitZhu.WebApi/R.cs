namespace LitZhu.WebApi
{
    public class R
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
        public static R Success(object? data = null)
        {
            return new R
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
        public static R Fail(object? data = null)
        {
            return new R
            {
                Code = 500,
                Message = "error",
                Data = data ?? ""
            };
        }
    }
}

namespace LitZhu.Image
{
    public interface IImageService
    {
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        Task<byte[]> GetImageAsync(string imageName);

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="imageData"></param>
        /// <param name="imageName"></param>
        /// <returns></returns>
        Task UploadImageAsync(byte[] imageData, string imageName);
    }
}
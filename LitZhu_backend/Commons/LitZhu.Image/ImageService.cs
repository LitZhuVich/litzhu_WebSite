namespace LitZhu.Image;

public class ImageService : IImageService
{
    public readonly string _imageStoragePath;

    public ImageService()
    {
        string projectRoot = AppContext.BaseDirectory;
        string desiredPath = Path.GetFullPath(Path.Combine(projectRoot, "..", "..", "..", "..", "Commons", "LitZhu.Image", "image"));
        _imageStoragePath = desiredPath;
    }

    public async Task<byte[]> GetImageAsync(string imageName)
    {
        var imagePath = Path.Combine(_imageStoragePath, imageName);
        if (File.Exists(imagePath))
        {
            using FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
            byte[] buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer, 0, (int)stream.Length);
            return buffer;
        }
        else
        {
            throw new FileNotFoundException(imagePath);
        }
    }

     public async Task UploadImageAsync(byte[] imageData, string imageName)
    {
        string filePath = Path.Combine(_imageStoragePath, imageName);
        await File.WriteAllBytesAsync(filePath, imageData);
    }
}

using LitZhu.Image;
using Microsoft.AspNetCore.Mvc;

namespace LitZhu.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController(
    ILogger<ImageController> _logger) : ControllerBase
{
    private readonly ImageService imageService = new();

    [HttpGet("{imageName}")]
    public async Task<IActionResult> GetImage(string imageName)
    {
        try
        {
            var imageBytes = await imageService.GetImageAsync(imageName);
            return new FileContentResult(imageBytes, "image/jpeg");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "获取图片失败");
            return BadRequest(R.Fail(e));
        }
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        _logger.LogInformation("上传文件:{file}", file);
        if (file != null && file.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] imageData = memoryStream.ToArray();

                string imageName = file.FileName;
                await imageService.UploadImageAsync(imageData, imageName);
                return Ok(R.Success("文件上传成功"));
            }
        }
        else
        {
            return BadRequest(R.Fail("没有上传文件文件"));
        }
    }
}

using Article.Domain;
using Article.Domain.DTO;
using Article.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LitZhu.WebApi.Controllers.Article;

[Route("api/[controller]")]
[ApiController]
public class TagController(
    ITagRepository _tagRepository,
    IMapper _mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TagDto>>> GetTags()
    {
        var tags = await _tagRepository.GetTagAsync();
        var tagsDto = _mapper.Map<List<TagDto>>(tags);
        return Ok(R.Success(tagsDto));
    }

    [HttpGet("Deleted")]
    [Authorize]
    public async Task<ActionResult<List<TagDto>>> GetTagsDeleted()
    {
        var tags = await _tagRepository.GetTagDeletedAsync();
        var tagsDto = _mapper.Map<List<TagDto>>(tags);
        return Ok(R.Success(tagsDto));
    }

    [HttpGet("{tagId}")]
    public async Task<ActionResult<TagDto>> FindTag(Guid tagId)
    {
        var tag = await _tagRepository.FindTagAsync(tagId);
        if (tag == null)
        {
            return NotFound(R.Fail("标签不存在"));
        }
        var tagDto = _mapper.Map<TagDto>(tag);
        return Ok(R.Success(tagDto));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<TagDto>> CreateTag(TagCreateDto createDto)
    {
        var tag = _mapper.Map<Tags>(createDto);
        var tagEntity = await _tagRepository.CreateTagAsync(tag);
        await _tagRepository.SaveTagAsync();

        var tagDto = _mapper.Map<TagDto>(tagEntity);
        return Ok(R.Success(tagDto));
    }

    [HttpPatch("{tagId}")]
    [Authorize]
    public async Task<ActionResult<TagDto>> UpdateTag(Guid tagId, Dictionary<string, string> updateDto)
    {
        var tagEntity = await _tagRepository.FindTagAsync(tagId);
        if (tagEntity == null)
        {
            return NotFound(R.Fail("标签不存在"));
        }

        tagEntity.Update(updateDto);

        var tagUpdatedEntity = await _tagRepository.UpdateTagAsync(tagEntity);
        await _tagRepository.SaveTagAsync();

        var tagUpdatedDto = _mapper.Map<TagDto>(tagUpdatedEntity);
        return Ok(R.Success(tagUpdatedDto));
    }

    [HttpDelete("{tagId}")]
    [Authorize]
    public async Task<IActionResult> DeleteTagSoft(Guid tagId)
    {
        try
        {
            await _tagRepository.DeleteTagSoftAsync(tagId);
            await _tagRepository.SaveTagAsync();
            return Ok(R.Success("删除成功"));
        }
        catch (Exception e)
        {
            return BadRequest(R.Fail("删除失败 :" + e.Message));
        }
    }
     
    [HttpDelete("{tagId}/True")]
    [Authorize]
    public async Task<IActionResult> DeleteTagTrue(Guid tagId)
    {
        try
        {
            await _tagRepository.DeleteTagTrueAsync(tagId);
            await _tagRepository.SaveTagAsync();
            return Ok(R.Success("删除成功"));
        }
        catch (Exception e)
        {
            return BadRequest(R.Fail("删除失败 :" + e.Message));
        }
    }
}

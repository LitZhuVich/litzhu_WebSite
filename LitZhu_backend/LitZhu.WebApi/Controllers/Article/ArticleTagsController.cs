using Article.Domain;
using Article.Domain.DTO;
using Article.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LitZhu.WebApi.Controllers.Article;

[Route("api/[controller]")]
[ApiController]
public class ArticleTagsController
(
    IArticleTagRepository _articleTagRepository,
    IArticleRepository _articleRepository,
    ITagRepository _tagRepository,
    IMapper _mapper
) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateArticleTags(ArticleTagsCreateDto createDto)
    {
        try
        {
            // 将文章和标签关联
            var articleTags = _mapper.Map<ArticleTags>(createDto);
            if (await _articleRepository.FindArticleAsync(createDto.articleId) == null)
            {
                return BadRequest(R.Fail("添加失败，文章不存在"));
            }
            if (await _tagRepository.FindTagAsync(createDto.tagId) == null)
            {
                return BadRequest(R.Fail("添加失败，标签不存在"));
            }
            await _articleTagRepository.CreateArticleTagAsync(articleTags);
            await _articleTagRepository.SaveArticleTagsAsync();
            return Ok(R.Success("添加成功"));
        }
        catch (Exception e)
        {
            return BadRequest(R.Fail($"添加失败{e.Message}"));
        }
    }

    [HttpDelete("{articleId}/{tagId}")]
    [Authorize]
    public async Task<IActionResult> DeleteArticleTags(Guid articleId, Guid tagId)
    {
        try
        {
            if (await _articleRepository.FindArticleAsync(articleId) == null)
            {
                return BadRequest(R.Fail("修改失败，文章不存在"));
            }
            if (await _tagRepository.FindTagAsync(tagId) == null)
            {
                return BadRequest(R.Fail("修改失败，标签不存在"));
            }
            var article = ArticleTags.Create(articleId, tagId);
            await _articleTagRepository.DeleteArticleTagsAsync(article);
            await _articleTagRepository.SaveArticleTagsAsync();
            return Ok(R.Success("删除成功"));
        }
        catch (Exception e)
        {
            return BadRequest(R.Fail($"删除失败{e.Message}"));
        }
    }

    [HttpGet("{articleId}/Tags")]
    public async Task<ActionResult<List<TagDto>>> GetArticleTags(Guid articleId)
    {
        var article = await _articleRepository.FindArticleAsync(articleId);
        if (article == null)
        {
            return NotFound(R.Fail("文章不存在"));
        }

        var tagsDto = _mapper.Map<List<TagDto>>(article.GetTags());
        return Ok(R.Success(tagsDto));
    }

    [HttpGet("{tagId}/Articles")]
    public async Task<ActionResult<List<ArticleDto>>> GetTagArticles(Guid tagId)
    {
        var tag = await _tagRepository.FindTagAsync(tagId);
        if (tag == null)
        {
            return NotFound(R.Fail("标签不存在"));
        }

        var articleDto = _mapper.Map<List<ArticleDto>>(tag.GetArticles());
        return Ok(R.Success(articleDto));
    }
}

using Article.Domain;
using Article.Domain.Entities;
using AutoMapper;
using LitZhu.WebApi.Controllers.Article.Dto;
using Microsoft.AspNetCore.Mvc;
using User.Domain.Entities;

namespace LitZhu.WebApi.Controllers.Article;

[Route("api/[controller]")]
[ApiController]
public class ArticleController(IArticleRepository _repository,IMapper _mapper) : ControllerBase
{
   
    [HttpGet]
    public async Task<ActionResult<List<ArticleDto>>> GetArticle()
    {
        var articles = await _repository.GetArticleAsync();
        var articlesDto = _mapper.Map<List<ArticleDto>>(articles);
        return Ok(ApiResponse.Success(articlesDto));
    }

    [HttpGet("Deleted")]
    public async Task<ActionResult<List<ArticleDto>>> GetArticleDeleted()
    {
        var articles = await _repository.GetArticleDeletedAsync();
        var articlesDto = _mapper.Map<List<ArticleDto>>(articles);
        return Ok(ApiResponse.Success(articlesDto));
    }

    [HttpGet("{articleId}")]
    public async Task<ActionResult<ArticleDto>> FindArticle(Guid articleId)
    {
        var articles = await _repository.FindArticleAsync(articleId);
        if (articles == null)
        {
            return NotFound(ApiResponse.Fail("文章不存在"));
        }
        var articlesDto = _mapper.Map<ArticleDto>(articles);
        return Ok(ApiResponse.Success(articlesDto));
    }

    [HttpPost]
    public async Task<ActionResult<ArticleDto>> CreateArticle(ArticleCreateDto createDto)
    {
        var article = _mapper.Map<Articles>(createDto);
        var articleEntity = await _repository.CreateArticleAsync(article);
        await _repository.SaveArticleAsync();

        var articleDto = _mapper.Map<ArticleDto>(articleEntity);
        return Ok(ApiResponse.Success(articleDto));
    }

    [HttpDelete("{articleId}")]
    public async Task<IActionResult> DeleteArticleSoft(Guid articleId)
    {
        try
        {
            await _repository.DeleteSoftArticleAsync(articleId);
            await _repository.SaveArticleAsync();
            return Ok(ApiResponse.Success("删除成功"));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse.Fail("删除失败 :" + e.Message));
        }
    }

    [HttpDelete("{articleId}/True")]
    public async Task<IActionResult> DeleteArticleTrue(Guid articleId)
    {
        try
        {
            await _repository.DeleteTrueArticleAsync(articleId);
            await _repository.SaveArticleAsync();
            return Ok(ApiResponse.Success("删除成功"));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse.Fail("删除失败 :" + e.Message));
        }
    }

    [HttpPatch("{articleId}")]
    public async Task<ActionResult<ArticleDto>> UpdateArticle(Guid articleId,ArticleUpdateDto updateDto)
    {
        var articleEntity = await _repository.FindArticleAsync(articleId);
        if (articleEntity == null)
        {
            return NotFound(ApiResponse.Fail("文章不存在"));
        }

        var article = _mapper.Map<ArticleUpdateDto>(updateDto);
        _mapper.Map(article, articleEntity); // 转换赋值

        var articleUpdatedEntity = await _repository.UpdateArticleAsync(articleEntity);
        await _repository.SaveArticleAsync();

        var articleUpdatedDto = _mapper.Map<ArticleDto>(articleUpdatedEntity);
        return Ok(ApiResponse.Success(articleUpdatedDto));
    }
}

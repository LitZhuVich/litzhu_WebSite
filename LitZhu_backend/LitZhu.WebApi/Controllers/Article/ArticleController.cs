using Article.Domain;
using Article.Domain.Entities;
using Article.Infrastructure;
using Article.WebApi.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace LitZhu.WebApi.Controllers.Article;

[Route("api/[controller]")]
[ApiController]
public class ArticleController(IArticleRepository _repository,IMapper _mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ArticleDto>>> GetArticleAll()
    {
        var articles = await _repository.GetArticleAllAsync();
        var articlesDto = _mapper.Map<List<ArticleDto>>(articles);
        return Ok(ApiResponse.Success(articlesDto));
    }

    [HttpGet("UnDeleted")]
    public async Task<ActionResult<List<ArticleDto>>> GetArticleAllUnDeleted()
    {
        var articles = await _repository.GetArticleAllUnDeletedAsync();
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
        await _repository.DeleteSoftArticleAsync(articleId);
        await _repository.SaveArticleAsync();
        return Ok(ApiResponse.Success("删除成功"));
    }

    [HttpDelete("{articleId}/True")]
    public async Task<IActionResult> DeleteArticleTrue(Guid articleId)
    {
        await _repository.DeleteTrueArticleAsync(articleId);
        await _repository.SaveArticleAsync();
        return Ok(ApiResponse.Success("删除成功"));
    }

    [HttpPatch("{articleId}")]
    public async Task<ActionResult<ArticleDto>> UpdateArticle([FromQuery] Guid articleId,ArticleUpdateDto updateDto)
    {
        var articleEntity = await _repository.FindArticleAsync(articleId);
        if (articleEntity == null)
        {
            return NotFound(ApiResponse.Fail("文章不存在"));
        }
        articleEntity.NotifyModified(); // 更新最后修改时间

        var article = _mapper.Map<ArticleUpdateDto>(updateDto);
        _mapper.Map(article, articleEntity); // 转换赋值

        var articleUpdatedEntity = await _repository.UpdateArticleAsync(articleEntity);
        await _repository.SaveArticleAsync();

        var articleUpdatedDto = _mapper.Map<ArticleDto>(articleUpdatedEntity);
        return Ok(ApiResponse.Success(articleUpdatedDto));
    }
}

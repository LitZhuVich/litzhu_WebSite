using Article.Domain;
using Article.Domain.DTO;
using Article.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LitZhu.WebApi.Controllers.Article;

[Route("api/[controller]")]
[ApiController]
public class ArticleController(IArticleRepository _repository, IMapper _mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ArticleDto>>> GetArticle([FromQuery] ArticleParametersDto parameters)
    {
        var articles = await _repository.GetArticleAsync(parameters);
        var articlesDto = _mapper.Map<List<ArticleDto>>(articles);
        return Ok(R.Success(articlesDto));
    }

    [HttpGet("Deleted")]
    [Authorize]
    public async Task<ActionResult<List<ArticleDto>>> GetArticleDeleted([FromQuery] ArticleParametersDto parameters)
    {
        var articles = await _repository.GetArticleDeletedAsync(parameters);
        var articlesDto = _mapper.Map<List<ArticleDto>>(articles);
        return Ok(R.Success(articlesDto));
    }

    [HttpGet("{articleId}")]
    public async Task<ActionResult<ArticleDto>> FindArticle(Guid articleId)
    {
        var articles = await _repository.FindArticleAsync(articleId);
        if (articles == null)
        {
            return NotFound(R.Fail("文章不存在"));
        }
        var articlesDto = _mapper.Map<ArticleDto>(articles);
        return Ok(R.Success(articlesDto));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ArticleDto>> CreateArticle(ArticleCreateDto createDto)
    {
        var article = _mapper.Map<Articles>(createDto);
        var articleEntity = await _repository.CreateArticleAsync(article.UserId, article);
        await _repository.SaveArticleAsync();

        var articleDto = _mapper.Map<ArticleDto>(articleEntity);
        return Ok(R.Success(articleDto));
    }

    [HttpDelete("{articleId}")]
    [Authorize]
    public async Task<IActionResult> DeleteArticleSoft(Guid articleId)
    {
        try
        {
            await _repository.DeleteSoftArticleAsync(articleId);
            await _repository.SaveArticleAsync();
            return Ok(R.Success("删除成功"));
        }
        catch (Exception e)
        {
            return BadRequest(R.Fail("删除失败 :" + e.Message));
        }
    }

    [HttpDelete("{articleId}/True")]
    [Authorize]
    public async Task<IActionResult> DeleteArticleTrue(Guid articleId)
    {
        try
        {
            await _repository.DeleteTrueArticleAsync(articleId);
            await _repository.SaveArticleAsync();
            return Ok(R.Success("删除成功"));
        }
        catch (Exception e)
        {
            return BadRequest(R.Fail("删除失败 :" + e.Message));
        }
    }

    [HttpPatch("{articleId}")]
    [Authorize]
    public async Task<ActionResult> UpdateArticle(Guid articleId, Dictionary<string, string> updateDto)
    {
        var article = await _repository.FindArticleAsync(articleId);
        if (article == null)
        {
            return NotFound(R.Fail("文章不存在"));
        }
        article.Update(updateDto); // 修改文章的数据

        var articleUpdatedEntity = await _repository.UpdateArticleAsync(article);
        await _repository.SaveArticleAsync();

        var articleDto = _mapper.Map<ArticleDto>(articleUpdatedEntity);
        return Ok(R.Success(articleDto));
    }
}

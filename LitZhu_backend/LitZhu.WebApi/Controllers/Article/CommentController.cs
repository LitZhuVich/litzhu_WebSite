using Article.Domain;
using Article.Domain.DTO;
using Article.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Domain;

namespace LitZhu.WebApi.Controllers.Article;

[Route("api/[controller]")]
[ApiController]
public class CommentController(
    IArticleRepository _articleRepository,
    IUserRepository _userRepository,
    ICommentRepository _commentRepository,
    IMapper _mapper) : ControllerBase
{
    [HttpGet("{articleId}/Comments")]
    public async Task<ActionResult<List<CommentDto>>> GetArticleComments(Guid articleId)
    {
        var article = await _articleRepository.FindArticleAsync(articleId);
        if (article == null)
        {
            return BadRequest(R.Fail("文章不存在"));
        }
        var comments = article.GetComments();
        var commentsDto = _mapper.Map<List<CommentDto>>(comments);
        return Ok(R.Success(commentsDto));
    }

    [HttpGet("{commentId}/Articles")]
    public async Task<ActionResult<ArticleDto>> GetCommentArticles(Guid commentId)
    {
        var comment = await _commentRepository.FindCommentAsync(commentId);
        if (comment == null)
        {
            return BadRequest(R.Fail("文章不存在"));
        }
        var articles = comment.GetArticle();
        var articleDto = _mapper.Map<ArticleDto>(articles);
        return Ok(R.Success(articleDto));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CommentDto>> CreateArticleComment(CommentCreateDto createDto)
    {
        if (await _userRepository.FindUserAsync(createDto.UserId) == null)
        {
            return BadRequest(R.Fail("用户不存在"));
        }
        if (await _articleRepository.FindArticleAsync(createDto.ArticleId) == null)
        {
            return BadRequest(R.Fail("文章不存在"));
        }
        var comment = _mapper.Map<Comments>(createDto);
        var commentEntity = await _commentRepository.CreateCommentAsync(comment);
        await _commentRepository.SaveCommentAsync();

        var commentDto = _mapper.Map<CommentDto>(commentEntity);
        return Ok(R.Success(commentDto));
    }

    [HttpDelete("{commentId}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment(Guid commentId)
    {
        if (await _commentRepository.FindCommentAsync(commentId) == null)
        {
            return BadRequest(R.Fail("评论不存在"));
        }

        await _commentRepository.DeleteCommentTrueAsync(commentId);
        return Ok("删除成功");
    }

    [HttpPatch("{commentId}")]
    [Authorize]
    public async Task<ActionResult<CommentDto>> UpdateComment(Guid commentId, Dictionary<string, string> updateDto)
    {
        var comment = await _commentRepository.FindCommentAsync(commentId);
        if (comment == null)
        {
            return BadRequest(R.Fail("评论不存在"));
        }

        comment.Update(updateDto);

        var tagUpdatedEntity = await _commentRepository.UpdateCommentAsync(comment);
        await _commentRepository.SaveCommentAsync();
        
        var commentDto = _mapper.Map<CommentDto>(tagUpdatedEntity);
        return Ok(R.Success(commentDto));
    }
}

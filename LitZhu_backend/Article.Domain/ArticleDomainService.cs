namespace Article.Domain;

public class ArticleDomainService(
    IArticleRepository _articleRepository
    )
{
    public async Task UploadMarkdown(string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
    }
}

// .net 博客怎么做到将上传md到后端然后显示在前端页面上
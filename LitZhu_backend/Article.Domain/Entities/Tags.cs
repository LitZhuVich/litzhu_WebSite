using LitZhu.DomainCommons.Models;

namespace Article.Domain.Entities;

public class Tags : AggregateRootEntity
{
    public string TagName { get; private set; } = string.Empty; // 文章标签名

    public List<ArticleTags> ArticleTagsList { get; private set; } = [];

    private Tags() { }

    public static Tags Create(string tagName)
    {
        return new Tags
        {
            TagName = tagName
        };
    }

    public void Update(Dictionary<string, string> update)
    {
        foreach (var item in update)
        {
            switch (item.Key)
            {
                case "tagName":
                    SetTagName(item.Value);
                    break;
                default:
                    throw new Exception("无效的参数");
            }
        }
    }

    /// <summary>
    /// 获取当前标签的文章
    /// </summary>
    /// <returns></returns>
    public List<Articles> GetArticles()
    {
        var articles = ArticleTagsList
            .Where(at => at.GetTagId() == Id)
            .Select(at => at.GetArticle())
            .ToList();
        return articles;
    }

    public void SetTagName(string tagName)
    {
        TagName = tagName;
    }
}

using Article.Domain;
using Article.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Article.Infrastructure;

public class TagRepository(ArticleDbContext _db) : ITagRepository
{
    public async Task<Tags> CreateTagAsync(Tags tag)
    {
        var tagCreateEntity = Tags.Create(tag.TagName);
        var newTag = await _db.Tags.AddAsync(tagCreateEntity);
        return newTag.Entity;
    }

    public async Task DeleteTagSoftAsync(Guid tagId)
    {
        var tag = await FindTagAsync(tagId);
        if (tag == null)
        {
            throw new Exception(nameof(DeleteTagSoftAsync) + "标签ID为空");
        }
        tag.SoftDelete();
    }

    public async Task DeleteTagTrueAsync(Guid tagId)
    {
        var tag = await FindTagAsync(tagId);
        if (tag == null)
        {
            throw new Exception(nameof(DeleteTagTrueAsync) + "标签ID为空");
        }
        _db.Tags.Remove(tag);
    }

    public async Task<Tags?> FindTagAsync(Guid tagId)
    {
        var tag = await _db.Tags.FirstOrDefaultAsync(x => x.Id == tagId);
        return tag;
    }

    public async Task<Tags?> FindTagAsync(string tagName)
    {
        var tag = await _db.Tags.FirstOrDefaultAsync(x => x.TagName == tagName);
        return tag;
    }

    public async Task<List<Tags>> GetTagAsync()
    {
        var tag = await _db.Tags.ToListAsync();
        return tag;
    }

    public async Task<List<Tags>> GetTagDeletedAsync()
    {
        var tag = await _db.Tags.IgnoreQueryFilters()
            .Where(x => x.IsDeleted == true).ToListAsync();
        return tag;
    }

    public async Task<bool> SaveTagAsync()
    {
        return await _db.SaveChangesAsync() >= 0;
    }

    public async Task<Tags> UpdateTagAsync(Tags tag)
    {
        if (await FindTagAsync(tag.Id) == null)
        {
            throw new Exception(nameof(UpdateTagAsync) + "标签ID为空");
        }
        _db.Tags.Update(tag);
        return tag;
    }
}

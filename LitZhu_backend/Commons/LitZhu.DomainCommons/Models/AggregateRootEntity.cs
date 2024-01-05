namespace LitZhu.DomainCommons.Models;

/// <summary>
/// 聚合根基类
/// </summary>
public class AggregateRootEntity : BaseEntity, IAggregateRoot, ISoftDelete, IHasCreationTime, IHasDeletionTime, IHasModificationTime
{
    public bool IsDeleted { get; private set; } = false;// 标记删除
    public DateTime CreationTime { get; private set; } = DateTime.Now; // 创建时间
    public DateTime? DeletionTime { get; private set; } // 删除时间
    public DateTime? LastModificationTime { get; private set; } = DateTime.Now; // 最后修改时间

    /// <summary>
    /// 软删除
    /// </summary>
    public void SoftDelete()
    {
        IsDeleted = true;
        DeletionTime = DateTime.Now;
    }

    /// <summary>
    /// 更新修改时间
    /// </summary>
    public void NotifyModified()
    {
        LastModificationTime = DateTime.Now;
    }

    /// <summary>
    /// 更新删除时间
    /// </summary>
    public void NotifyDeleted()
    {
        DeletionTime = DateTime.Now;
    }
}

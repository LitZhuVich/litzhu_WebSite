namespace LitZhu.DomainCommons.Models;

/// <summary>
/// 实体基类
/// </summary>
public class BaseEntity : IBaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
}

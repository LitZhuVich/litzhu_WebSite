namespace LitZhu.DomainCommons.Models;

/// <summary>
/// 实体基类接口
/// </summary>
public interface IBaseEntity
{
    Guid Id { get; }

    //public long AutoIncId { get; }
    //在MySQL中，用Guid做物理主键性能非常低。因此，物理上用自增的列做主键。但是逻辑上、包括两表之间的关联都用Guid。
    //2024-1-3 暂时没有使用自增列。
}
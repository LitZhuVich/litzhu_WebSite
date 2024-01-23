using LitZhu.DomainCommons.Models;

namespace User.Domain.DTO;

public class RoleDto : IBaseEntity, IHasCreationTime, IHasModificationTime
{
    public Guid Id { get; private set; }
    public string? RoleName { get; private set; } // 角色名
    public string? RoleDesc { get; private set; } // 角色描述
    public DateTime CreationTime { get; private set; }
    public DateTime? LastModificationTime { get; private set; }
}

public record RoleCreateDto(string RoleName, string RoleDesc);

public record RoleUpdateDto(string? RoleName, string? RoleDesc);
namespace User.Domain.Entities;

public class UserRoles
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
    public Users Users { get; private set; }
    public Roles Roles { get; private set; }

    private UserRoles() { }

    public static UserRoles Create(Guid userId, Guid roleId)
    {
        return new UserRoles
        {
            UserId = userId,
            RoleId = roleId
        };
    }

    public Guid GetUserId() => UserId;
    public Guid GetRoleId() => RoleId;
    public Users GetUser() => Users;
    public Roles GetRole() => Roles;
}

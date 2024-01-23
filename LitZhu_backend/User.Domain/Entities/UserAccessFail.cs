using LitZhu.DomainCommons.Models;

namespace User.Domain.Entities;

public class UserAccessFail : BaseEntity, IHasCreationTime
{
    public Guid UserId { get; init; } // 用户ID
    public Users? User { get; init; } // 用户
    public bool IsLockOut { get; private set; } = false; // 是否锁定
    public DateTime? LockOutEnd { get; private set; } // 锁定截止时间
    public int AccessFailedCount { get; private set; } // 登录失败次数
    public DateTime CreationTime { get; private set; } = DateTime.Now; // 锁定时间

    public static UserAccessFail Create(Guid userId)
    {
        return new UserAccessFail { UserId = userId };
    }

    /// <summary>
    /// 重置用户状态
    /// </summary>
    public void Reset()
    {
        IsLockOut = false;
        LockOutEnd = null;
        AccessFailedCount = 0;
    }

    /// <summary>
    /// 处理一次“登陆失败”
    /// </summary>
    public void Fail()
    {
        AccessFailedCount++;
        // 失败次数大于等于10此则锁定，锁定时间为3分钟
        if (AccessFailedCount >= 10)
        {
            IsLockOut = true;
            LockOutEnd = DateTime.Now.AddMinutes(3);
        }
    }

    /// <summary>
    /// 判断是否已锁定
    /// </summary>
    /// <returns></returns>
    public bool IsLockOuted()
    {
        // 已锁定
        if (IsLockOut)
        {
            // 已经过了锁定时间
            if (DateTime.Now > LockOutEnd)
            {
                Reset();
                return false;
            }
            return true;
        }
        return false;
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Domain;
using User.Domain.DTO;
using User.Domain.Entities;

namespace LitZhu.WebApi.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class UserRolesController
	(
        IUserRolesRepository _userRolesRepository,
        IUserRepository _userRepository,
        IRoleRepository _roleRepository,
        IMapper _mapper
    ) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateUserRoles(UserRolesDto createDto)
    {
		try
        {
			var userRoles = _mapper.Map<UserRoles>(createDto);
            if (await _userRepository.FindUserAsync(userRoles.UserId) == null)
            {
                return BadRequest(R.Fail("添加失败:用户不存在"));
            }
            if (await _roleRepository.FindRoleAsync(userRoles.RoleId) == null)
            {
                return BadRequest(R.Fail("添加失败:角色不存在"));
            }
            await _userRolesRepository.CreateUserRoleAsync(userRoles);
            await _userRolesRepository.SaveUserRolesAsync();
            return Ok(R.Success("添加成功"));
        }
		catch (Exception e)
		{
			return BadRequest(R.Fail($"添加失败:{e.Message}"));
		}
    }

    [HttpDelete("{userId}/{roleId}")]
    [Authorize]
    public async Task<IActionResult> DeleteUserRoles(Guid userId, Guid roleId)
    {
        try
        {
            if (await _userRepository.FindUserAsync(userId) == null)
            {
                return BadRequest(R.Fail("修改失败，用户不存在"));
            }
            if (await _roleRepository.FindRoleAsync(roleId) == null)
            {
                return BadRequest(R.Fail("修改失败，角色不存在"));
            }
            var userRoles = UserRoles.Create(userId, roleId);
            await _userRolesRepository.DeleteUserRolesAsync(userRoles);
            await _userRolesRepository.SaveUserRolesAsync();
            return Ok(R.Success("删除成功"));
        }
        catch (Exception e)
        {
            return BadRequest(R.Fail($"删除失败{e.Message}"));
        }
    }

    [HttpGet("{userId}/Roles")]
    public async Task<IActionResult> GetUserRoles(Guid userId)
    {
        var user = await _userRepository.FindUserAsync(userId);
        if (user == null)
        {
            return BadRequest(R.Fail("用户不存在"));
        }

        var userRoles = _mapper.Map<List<RoleDto>>(user.GetRoles());
        return Ok(R.Success(userRoles));
    }

    [HttpGet("{roleId}/Users")]
    public async Task<IActionResult> GetRoleUsers(Guid roleId)
    {
        Roles? role = await _roleRepository.FindRoleAsync(roleId);
        if (role == null)
        {
            return BadRequest(R.Fail("角色不存在"));
        }

        var roleRoles = _mapper.Map<List<UserDto>>(role.GetUsers());
        return Ok(R.Success(roleRoles));
    }
}

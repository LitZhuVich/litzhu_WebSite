using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Domain;
using User.Domain.DTO;
using User.Domain.Entities;

namespace LitZhu.WebApi.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class RoleController(IRoleRepository _roleRepository, IMapper _mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<RoleDto>>> GetRole()
    {
        var roles = await _roleRepository.GetRoleAsync();
        var rolesDto = _mapper.Map<List<RoleDto>>(roles);
        return Ok(R.Success(rolesDto));
    }

    [HttpGet("Deleted")]
    public async Task<ActionResult<List<RoleDto>>> GetRoleDeleted()
    {
        var roles = await _roleRepository.GetRoleDeletedAsync();
        var rolesDto = _mapper.Map<List<RoleDto>>(roles);
        return Ok(R.Success(rolesDto));
    }

    [HttpGet("{roleId}")]
    public async Task<ActionResult<RoleDto>> FindRole(Guid roleId)
    {
        var role = await _roleRepository.FindRoleAsync(roleId);
        if (role == null)
        {
            return BadRequest(R.Fail("角色不存在"));
        }
        var roleDto = _mapper.Map<RoleDto>(role);
        return Ok(R.Success(roleDto));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<RoleDto>> CreateRole(RoleCreateDto createDto)
    {
        var role = _mapper.Map<Roles>(createDto);
        var result = await _roleRepository.CreateRoleAsync(role);
        await _roleRepository.SaveRoleAsync();

        var roleDto = _mapper.Map<RoleDto>(result);
        return Ok(R.Success(roleDto));
    }

    [HttpDelete("{roleId}")]
    [Authorize]
    public async Task<IActionResult> DeleteRoleSoft(Guid roleId)
    {
        try
        {
            await _roleRepository.DeleteRoleSoftAsync(roleId);
            await _roleRepository.SaveRoleAsync();
            return Ok(R.Success("删除成功"));
        }
        catch (Exception e)
        {
            return BadRequest(R.Fail("删除失败:" + e.Message));
        }
    }

    [HttpDelete("{roleId}/True")]
    [Authorize]
    public async Task<IActionResult> DeleteRoleTrue(Guid roleId)
    {
        try
        {
            await _roleRepository.DeleteRoleTrueAsync(roleId);
            await _roleRepository.SaveRoleAsync();
            return Ok(R.Success("删除成功"));
        }
        catch (Exception e)
        {
            return BadRequest(R.Fail("删除失败:" + e.Message));
        }
    }

    [HttpPatch("{roleId}")]
    [Authorize]
    public async Task<ActionResult<RoleDto>> UpdateRole(Guid roleId, Dictionary<string, string> updateDto)
    {
        var role = await _roleRepository.FindRoleAsync(roleId);
        if (role == null)
        {
            return BadRequest(R.Fail("角色不存在"));
        }

        role.Update(updateDto);

        var roleUpdated = await _roleRepository.UpdateRoleAsync(role);
        await _roleRepository.SaveRoleAsync();

        var roleUpdatedDto = _mapper.Map<RoleDto>(roleUpdated);
        return Ok(R.Success(roleUpdatedDto));
    }
}

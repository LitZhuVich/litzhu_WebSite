using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Domain;
using User.Domain.DTO;
using User.Domain.Entities;

namespace LitZhu.WebApi.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserRepository _userRepository, IMapper _mapper) : ControllerBase
{
    [HttpPost("Username")]
    [Authorize]
    public async Task<ActionResult<UserDto>> CreateUserByUsername(UserCreateByUsernameDto createDto)
    {
        var user = _mapper.Map<Users>(createDto);
        var result = await _userRepository.CreateUserAsync(user);
        await _userRepository.SaveUserAsync();

        var userDto = _mapper.Map<UserDto>(result);
        return Ok(R.Success(userDto));
    }

    [HttpPost("Email")]
    [Authorize]
    public async Task<ActionResult<UserDto>> CreateUserByEMail(UserCreateByEmailDto createDto)
    {
        var user = _mapper.Map<Users>(createDto);
        var result = await _userRepository.CreateUserAsync(user);
        await _userRepository.SaveUserAsync();
        return Ok(R.Success(result));
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDto>> FindUser(Guid userId)
    {
        var user = await _userRepository.FindUserAsync(userId);
        if (user == null)
        {
            return BadRequest(R.Fail("用户不存在"));
        }
        var userDto = _mapper.Map<UserDto>(user);
        return Ok(R.Success(userDto));
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetUser()
    {
        var users = await _userRepository.GetUserAsync();
        var usersDto = _mapper.Map<List<UserDto>>(users);
        return Ok(R.Success(usersDto));
    }
    
    [HttpGet("Deleted")]
    [Authorize]
    public async Task<ActionResult<List<UserDto>>> GetUserDeleted()
    {
        var users = await _userRepository.GetUserDeletedAsync();
        var usersDto = _mapper.Map<List<UserDto>>(users);
        return Ok(R.Success(usersDto));
    }

    [HttpDelete("{userId}")]
    [Authorize]
    public async Task<IActionResult> DeleteUserSoft(Guid userId)
    {
        try
        {
            await _userRepository.DeleteUserSoftAsync(userId);
            await _userRepository.SaveUserAsync();
            return Ok(R.Success("删除成功"));
        }
        catch (Exception e)
        {
            return BadRequest(R.Fail("删除失败 :" + e.Message));
        }
    }

    [HttpDelete("{userId}/True")]
    [Authorize(Roles = "管理员")]
    public async Task<IActionResult> DeleteUserTrue(Guid userId)
    {
        try
        {
            await _userRepository.DeleteUserTrueAsync(userId);
            await _userRepository.SaveUserAsync();
            return Ok(R.Success("删除成功"));
        }
        catch (Exception e)
        {
            return BadRequest(R.Fail("删除失败 :" + e.Message));
        }
    }

    [HttpPatch("{userId}")]
    public async Task<ActionResult<UserDto>> UpdateUser(Guid userId, Dictionary<string, string> updateDto)
    {
        var user = await _userRepository.FindUserAsync(userId);
        if (user == null)
        {
            return BadRequest(R.Fail("用户不存在"));
        }
        user.Update(updateDto);

        var userUpdated = await _userRepository.UpdateUserAsync(user);
        await _userRepository.SaveUserAsync();

        var userUpdatedDto = _mapper.Map<UserDto>(userUpdated);
        return Ok(R.Success(userUpdatedDto));
    }
}

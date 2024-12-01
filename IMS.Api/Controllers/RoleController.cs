using IMS.Application.ServiceInterface;
using IMS.Domain.Models;
using IMS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _role;
        public RoleController(IRole role)
        {
            _role = role;
        }
        [HttpGet("GetAllRole")]
        public async Task<IActionResult> GetAllRole()
        {
            try
            {
                var data = await _role.GetAllRoleAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost("CreateOrUpdateRole")]
        public async Task<IActionResult> CreateOrUpdateRole(AspNetRoleVM role)
        {
            try
            {
                var saveData =await _role.CreateOrUpdateRole(role);
                return saveData ?Ok(saveData) : BadRequest();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet("GetRoleById/{id}")]
        public async Task<IActionResult>GetRoleById(string id)
        {
            try
            {
                return Ok(await _role.GetRoleByRoleId(id));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}

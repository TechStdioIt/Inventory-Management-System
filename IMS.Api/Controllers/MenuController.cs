using IMS.Application.ServiceInterface;
using IMS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IIMSMenu _menu;
        public MenuController(IIMSMenu menu)
        {
            _menu = menu;  
        }
        [HttpGet("GetAllMenu")]
        public async Task<IActionResult> GetAllMenu()
        {
            try
            {
                var result = await _menu.GetAllMenu();
                return Ok(new
                {
                    Status = 200,
                    Message = "Reviews retrieved successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = "An error occurred while retrieving reviews",
                    ErrorDetails = ex.Message
                });
            }
        }
        [HttpPost("CreateOrUpdateMenu")]
        public async Task<IActionResult> CreateOrUpdateMenu(IMSMenuVm data)
        {
            try
            {
                var result = await _menu.CreateOrUpdateMenu(data);
                return Ok(new
                {
                    Status = 200,
                    Message = "Reviews retrieved successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = "An error occurred while retrieving reviews",
                    ErrorDetails = ex.Message
                });
            }
        }
        [HttpGet("GetMenuById")]
        public async Task<IActionResult> GetMenuById(int id)
        {
            try
            {
                var result = await _menu.GetMenuById(id);
                return Ok(new
                {
                    Status = 200,
                    Message = "Reviews retrieved successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = "An error occurred while retrieving reviews",
                    ErrorDetails = ex.Message
                });
            }
        }
        [HttpGet("DeleteMenu")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            try
            {
                var result = await _menu.DeleteMenu(id);
                return Ok(new
                {
                    Status = 200,
                    Message = "Reviews retrieved successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = "An error occurred while retrieving reviews",
                    ErrorDetails = ex.Message
                });
            }
        }

    }
}

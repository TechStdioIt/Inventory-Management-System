using IMS.Application.ServiceInterface;
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
        public async Task<IActionResult> GetAllMenus()
        {

            try
            {
                var menu = await _menu.GetMenuAsync();
                return Ok(menu); // This will serialize the hierarchical data
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

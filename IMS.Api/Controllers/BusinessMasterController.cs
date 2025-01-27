using IMS.Application.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessMasterController : ControllerBase
    {
        private readonly IBusiness _business;
        public BusinessMasterController(IBusiness business)
        {
          _business = business;
        }

        [HttpGet("GetPackageData")]
        public async Task<IActionResult> GetPackageData()
        {
            try
            {
                var result = await _business.GetPackageData();
                var response = new
                {
                    status = 200,
                    message = "Success",
                    data = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    status = 500,
                    message = ex.Message
                };
                return StatusCode(500, errorResponse);
            }
        }
    }
}

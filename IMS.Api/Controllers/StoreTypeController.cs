using IMS.Application.ServiceInterface;
using IMS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreTypeController : ControllerBase
    {
        private readonly IStoreType _storetype;

        public StoreTypeController(IStoreType storetype)
        {
            _storetype = storetype;
        }
        [HttpPost("CreateOrUpdateStore")]
        public async Task<IActionResult> CreateOrUpdateStore(StoreTypeVM data)
        {
            try
            {
                var result = await _storetype.CreateOrUpdateStore(data);
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
        [HttpGet("GetDataById")]
        public async Task<IActionResult> GetDataById(int id)
        {
            try
            {
                var result = await _storetype.GetDataById(id);
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
        [HttpDelete("DeleteDataById")]
        public async Task<IActionResult> DeleteDataById(int id)
        {
            try
            {
                var result = await _storetype.DeleteDataById(id);
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

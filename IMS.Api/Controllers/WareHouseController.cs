using IMS.Application.ServiceInterface;
using IMS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseController : ControllerBase
    {
        private readonly  IWareHouse _wareHouse;

        public WareHouseController(IWareHouse wareHouse)
        {
            _wareHouse = wareHouse;
        }
        [HttpGet("GetAllWareHouse")]
        public async Task<IActionResult> GetAllWareHouse()
        {
            try
            {
                var result = await _wareHouse.GetAllWareHouse();
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
        [HttpPost("CreateOrUpdateWareHouse")]
        public async Task<IActionResult> CreateOrUpdateWareHouse(WareHouseVM data)
        {
            try
            {
                var result = await _wareHouse.CreateOrUpdateWareHouse(data);
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
        [HttpGet("GetWareHouseById")]
        public async Task<IActionResult> GetWareHouseById(int id)
        {
            try
            {
                var result = await _wareHouse.GetWareHouseById(id);
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
        [HttpGet("DeleteWareHouse")]
        public async Task<IActionResult> DeleteWareHouse(int id)
        {
            try
            {
                var result = await _wareHouse.DeleteWareHouse(id);
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

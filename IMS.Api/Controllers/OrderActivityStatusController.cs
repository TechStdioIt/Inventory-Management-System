using IMS.Application.ServiceInterface;
using IMS.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderActivityStatusController : ControllerBase
    {
        
        private readonly IOrderActivityStatus _orderActivityStatus;

        public OrderActivityStatusController(IOrderActivityStatus orderActivityStatus)
        {
            _orderActivityStatus = orderActivityStatus;
        }
        [HttpDelete("GetAllOrderActivityStatus")]
        public async Task<IActionResult> GetAllOrderActivityStatus()
        {
            try
            {
                var result = await _orderActivityStatus.GetAllOrderActivityStatus();
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
        [HttpPost("CreateOrUpdateOrderActivityStatus")]
        public async Task<IActionResult> CreateOrUpdateOrderActivityStatus(OrderActivityStatusVM data)
        {
            try
            {
                var result = await _orderActivityStatus.CreateOrUpdateOrderActivityStatus(data);
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
        [HttpGet("GetOrderActivityStatusById")]
        public async Task<IActionResult> GetOrderActivityStatusById(int id)
        {
            try
            {
                var result = await _orderActivityStatus.GetOrderActivityStatusById(id);
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
    [HttpDelete("DeleteOrderActivityStatus")]
    public async Task<IActionResult> DeleteOrderActivityStatus(int id)
    {
        try
        {
            var result = await _orderActivityStatus.DeleteOrderActivityStatus(id);
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

using IMS.Application.ServiceInterface;
using IMS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseTypeController : ControllerBase
    {
        private readonly IPurchaseType _purchaseType;

        public PurchaseTypeController(IPurchaseType purchaseType )
        {
            _purchaseType = purchaseType;
        }
        [HttpGet("GetAllPurchaseType")]
        public async Task<IActionResult> GetAllPurchaseType()
        {
            try
            {
                var result = await _purchaseType.GetAllPurchaseType();
                return Ok(new
                {
                    Status = 200,
                    Message = "Data retrieved successfully",
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
        [HttpPost("CreateOrUpdatePurchaseType")]
        public async Task<IActionResult> CreateOrUpdatePurchaseType(PurchaseTypeVM data)
        {
            try
            {
                var result = await _purchaseType.CreateOrUpdatePurchaseType(data);
                return Ok(new
                {
                    Status = 200,
                    Message = "Data retrieved successfully",
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
        [HttpGet("GetPurchaseTypeById")]
        public async Task<IActionResult> GetPurchaseTypeById(int id)
        {
            try
            {
                var result = await _purchaseType.GetPurchaseTypeById(id);
                return Ok(new
                {
                    Status = 200,
                    Message = "Data retrieved successfully",
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
        [HttpDelete("DeletePurchaseType")]
        public async Task<IActionResult> DeletePurchaseType(int id)
        {
            try
            {
                var result = await _purchaseType.DeletePurchaseType(id);
                return Ok(new
                {
                    Status = 200,
                    Message = "Data retrieved successfully",
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

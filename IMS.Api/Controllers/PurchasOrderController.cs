using IMS.Application.ServiceInterface;
using IMS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasOrderController : ControllerBase
    {
        private readonly IPurchasOrder _purchaseOrder;
        public PurchasOrderController(IPurchasOrder purchaseOrder)
        {
            _purchaseOrder = purchaseOrder;
        }

        [HttpPost("CreateOrUpdatePurchaseOrder")]
        public async Task<IActionResult> CreateOrUpdateDoctorPrescription(PurchasOrderVM data)
        {
            try
            {
                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                var result = await _purchaseOrder.CreateOrUpdatePurchaseOrder(jsonData);
                return Ok(new
                {
                    Status = 200,
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
        [HttpGet("GetAllpurchaseOrder")]
        public async Task<IActionResult> GetAllpurchaseOrder()
        {
            try
            {
                var result = await _purchaseOrder.GetAllpurchaseOrder();
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

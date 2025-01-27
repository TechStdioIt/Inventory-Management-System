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
        private readonly IPurchasOrder _purchasOrder;
        public PurchasOrderController(IPurchasOrder purchasOrder)
        {
            _purchasOrder = purchasOrder;
        }

        [HttpPost("CreateOrUpdatePurchaseOrder")]
        public async Task<IActionResult> CreateOrUpdateDoctorPrescription(PurchasOrderVM data)
        {
            try
            {
                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                var result = await _purchasOrder.CreateOrUpdatePurchaseOrder(jsonData);
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
    }
}

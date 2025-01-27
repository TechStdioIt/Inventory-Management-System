using IMS.Application.ServiceInterface;
using IMS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBank _bank;

        public BankController(IBank bank)
        {
            _bank = bank;
        }
        [HttpGet("GetAllBank")]
        public async Task<IActionResult> GetAllBank()
        {
            try
            {
                var result = await _bank.GetAllBank();
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
        [HttpPost("CreateOrUpdateBank")]
        public async Task<IActionResult> CreateOrUpdateBank(BankVM data)
        {
            try
            {
                var result = await _bank.CreateOrUpdateBank(data);
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
        [HttpGet("GetBankById")]
        public async Task<IActionResult> GetBankById(int id)
        {
            try
            {
                var result = await _bank.GetBankById(id);
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
        [HttpDelete("DeleteBank")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            try
            {
                var result = await _bank.DeleteBank(id);
                return Ok(new
                {
                    Status = 200,
                    Message = "Deleted successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = "An error occurred",
                    ErrorDetails = ex.Message
                });
            }
        }
    }
}

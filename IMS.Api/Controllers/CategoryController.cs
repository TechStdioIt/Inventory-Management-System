using IMS.Application.ServiceInterface;
using IMS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _category;

        public CategoryController(ICategory category)
        { 
            _category = category;
        }
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var result = await _category.GetAllCategory();
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
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate(CategoryVM data)
        {
            try
            {
                var result = await _category.CreateOrUpdate(data);
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
        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var result = await _category.GetCategoryById(id);
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

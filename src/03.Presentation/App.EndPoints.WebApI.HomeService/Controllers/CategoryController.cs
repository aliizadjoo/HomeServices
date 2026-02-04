using App.Domain.Core.Contract.CategoryAgg.AppService;
using App.Domain.Core.Contract.CategoryAgg.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.WebApI.HomeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryAppService _categoryAppService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageSize, [FromQuery] int pageNumber, CancellationToken cancellationToken) 
        {
           var result= await _categoryAppService.GetAll(pageSize, pageNumber, null, cancellationToken);

            if (result.IsSuccess) 
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}

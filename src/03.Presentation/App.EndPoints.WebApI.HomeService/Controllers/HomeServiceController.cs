using App.Domain.AppServices.HomeserviceAgg;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.WebApI.HomeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeServiceController(IHomeserviceAppService _homeserviceAppService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageSize=10, [FromQuery] int pageNumber=1, CancellationToken cancellationToken=default) 
        {
            var result = await _homeserviceAppService.GetAll(pageSize, pageNumber,  cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}

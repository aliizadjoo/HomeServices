using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Dtos.AccountAgg;
using App.EndPoints.WebApI.HomeService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.WebApI.HomeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountAppService _accountAppService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel model, CancellationToken cancellationToken)       
        {
            
            var dto = new UserRegisterDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
                CityId = model.CityId,
              
            };

            var result = await _accountAppService.Register(dto, cancellationToken);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    Errors = result.Errors.Select(e => e.Description)
                });
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                Message = "ثبت نام با موفقیت انجام شد"
            });
        }
    }
}

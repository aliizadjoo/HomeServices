using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Contract.ExpertAgg.AppService;
using App.Domain.Core.Entities;
using App.EndPoints.MVC.HomeService.Areas.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HomeService.Areas.Expert.Controllers
{

    [Area(AreaConstants.Expert)]
    [Authorize(Roles = RoleConstants.Expert)]
    public class ProfileController
        (IExpertAppService _expertAppService
        , IAccountAppService _accountAppService
        ,ILogger<ProfileController> _logger
        ) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellation)
        {
            var appuserId = _accountAppService.GetUserId(User);

            if (appuserId <= 0)
            {
                _logger.LogWarning(" آیدی معتبر نیست.");
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }


            var result = await _expertAppService.GetProfile(appuserId, cancellation);
            if (!result.IsSuccess)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }


            return View(result.Data);
        }
    }
}

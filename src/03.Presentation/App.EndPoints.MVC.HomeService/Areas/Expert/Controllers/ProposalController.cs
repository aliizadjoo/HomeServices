using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Contract.OrderAgg.AppService;
using App.Domain.Core.Contract.ProposalAgg.AppService;
using App.Domain.Core.Dtos.ProposalAgg;
using App.Domain.Core.Entities;
using App.EndPoints.MVC.HomeService.Areas.Constants;
using App.EndPoints.MVC.HomeService.Areas.Expert.Models;
using App.EndPoints.MVC.HomeService.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HomeService.Areas.Expert.Controllers
{
    [Area(AreaConstants.Expert)]
    [Authorize(Roles = RoleConstants.Expert)]
    public class ProposalController
        (IOrderAppService  _orderAppService
        ,IAccountAppService _accountAppService
        , IProposalAppService _proposalAppService,
        ILogger<ProposalController> _logger
        
        ) 
        : Controller
    {



       
        public async Task<IActionResult> MyProposals(CancellationToken cancellationToken)
        {
          
            int expertId = _accountAppService.GetExpertId(User);

            if (expertId <= 0)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

         
            var result = await _proposalAppService.GetExpertProposals(expertId, cancellationToken);

          
            if (result.IsSuccess && result.Data != null)
            {
                foreach (var proposal in result.Data)
                {
                    proposal.PersianExecutionDate = proposal.ExecutionDate.ToPersianDate();
                }
            }

            return View(result.Data ?? new List<ExpertProposalDto>());
        }

        public async Task<IActionResult> SubmitProposal(int orderId, CancellationToken cancellationToken)
        {
            ProposalViewModel proposalViewModel = new ProposalViewModel() { OrderId = orderId };

            var result = await _orderAppService.GetOrderSummary(orderId, cancellationToken);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Index" , "order");
            }


            var viewModel = new ProposalViewModel
            {
                OrderId = orderId,
                OrderTitle = result.Data.HomeServiceName,
                BasePrice = result.Data.BasePrice
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitProposal(ProposalViewModel model, CancellationToken cancellationToken)
        {

            if (model.Price < model.BasePrice)
            {
                ModelState.AddModelError("Price", $"قیمت پیشنهادی نمی‌تواند کمتر از {model.BasePrice:N0} ریال باشد.");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }


        
            int expertId = _accountAppService.GetExpertId(User);
            if (expertId <= 0)
            {
               
                _logger.LogWarning("User {UserId} tried to create an order but has no CustomerId claim.", User.Identity.Name);

                TempData["ErrorMessage"] = "اطلاعات حساب کاربری شما ناقص است. لطفا دوباره وارد شوید.";
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            DateTime suggestedDate = model.SuggestedDateShamsi.ToGregorianDate();


            var proposalDto = new ProposalCreateDto
            {
                OrderId = model.OrderId,
                Price = model.Price,
                Description = model.Description,
                SuggestedDate = suggestedDate,
                ExpertId = expertId
            };


            var result = await _proposalAppService.Create(proposalDto, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;

                return RedirectToAction("Index" , "order");
            }


            TempData["ErrorMessage"] = result.Message;
            return View(model);
        }



    }
}

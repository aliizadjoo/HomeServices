using App.Domain.Core._common;
using App.Domain.Core.Contract.ExpertAgg.Repository;
using App.Domain.Core.Contract.OrderAgg.Repository;
using App.Domain.Core.Contract.ProposalAgg.Repository;
using App.Domain.Core.Contract.ProposalAgg.Service;
using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Dtos.ProposalAgg;
using App.Domain.Core.Entities;
using App.Domain.Core.Enums.OrderAgg;
using App.Domain.Core.Enums.ProposalAgg;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Services.ProposalAgg
{
    public class ProposalService
        (IProposalRepository _proposalRepository 
        , ILogger<ProposalService> _logger 
        , IOrderRepository _orderRepository
        ,IExpertRepository _expertRepository
        ) : IProposalService
    {
        //public async Task<Result<bool>> Create(ProposalCreateDto proposalCreateDto, CancellationToken cancellationToken)
        //{

        //    _logger.LogInformation("Attempting to create a new proposal for OrderId: {OrderId} by ExpertId: {ExpertId}",
        //        proposalCreateDto.OrderId, proposalCreateDto.ExpertId);


        //    var executionDateTime = proposalCreateDto.SuggestedDate.Date.Add(proposalCreateDto.ExecutionTime);


        //    if (executionDateTime < DateTime.Now)
        //    {
        //        return Result<bool>.Failure("زمان انتخاب شده نمی‌تواند در گذشته باشد. لطفاً ساعت و تاریخ معتبری را انتخاب کنید.");
        //    }

        //    decimal? basePrice = await _orderRepository.GetBasePriceByOrderId(proposalCreateDto.OrderId , cancellationToken);
        //    if (proposalCreateDto.Price< basePrice)
        //    {
        //        return Result<bool>.Failure("مبلغ پیشنهادی نباید کمتر از مبلغ پایه باشد .");
        //    }


        //    var affectedRows = await _proposalRepository.Create(proposalCreateDto, cancellationToken);


        //    if (affectedRows > 0)
        //    {
        //        _logger.LogInformation("Proposal successfully created for OrderId: {OrderId} with {Rows} affected row(s).",
        //            proposalCreateDto.OrderId, affectedRows);

        //        return Result<bool>.Success(true, "پیشنهاد شما با موفقیت ثبت شد و برای مشتری ارسال گردید.");
        //    }


        //    _logger.LogWarning("Failed to save the proposal in the database for OrderId: {OrderId}.",
        //        proposalCreateDto.OrderId);

        //    return Result<bool>.Failure("خطایی در هنگام ذخیره پیشنهاد در سیستم رخ داد. لطفاً مجدداً تلاش کنید.");
        //}

        public async Task<Result<bool>> Create(ProposalCreateDto proposalCreateDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting proposal creation for OrderId: {OrderId} by ExpertId: {ExpertId}",
                proposalCreateDto.OrderId, proposalCreateDto.ExpertId);

    
            var order = await _orderRepository.GetOrderSummary(proposalCreateDto.OrderId, cancellationToken);

            if (order == null)
                return Result<bool>.Failure("سفارش مورد نظر یافت نشد.");

            if (order.Status != OrderStatus.WaitingForProposals)
            {
                return Result<bool>.Failure("مهلت ارسال پیشنهاد برای این سفارش به پایان رسیده یا وضعیت آن تغییر کرده است.");
            }

            bool hasActiveProposal = await _proposalRepository.HasProposal(proposalCreateDto.OrderId, proposalCreateDto.ExpertId, cancellationToken);
            if (hasActiveProposal)
            {
                return Result<bool>.Failure("شما قبلاً برای این سفارش پیشنهاد ارسال کرده‌اید.");
            }

           
            bool expertHasSkill = await _expertRepository.HasSkill(proposalCreateDto.ExpertId, order.HomeServiceId, cancellationToken);

            if (!expertHasSkill)
            {
                return Result<bool>.Failure("شما مهارت لازم برای انجام این نوع سرویس را در پروفایل خود ندارید.");
            }


            var executionDateTime = proposalCreateDto.SuggestedDate.Date.Add(proposalCreateDto.ExecutionTime);
            if (executionDateTime < DateTime.Now)
            {
                return Result<bool>.Failure("زمان انتخاب شده نمی‌تواند در گذشته باشد.");
            }

       
            if ( proposalCreateDto.Price < order.BasePrice)
            {
                return Result<bool>.Failure($"مبلغ پیشنهادی نباید کمتر از قیمت پایه ({order.BasePrice}) باشد.");
            }

            var affectedRows = await _proposalRepository.Create(proposalCreateDto, cancellationToken);

            if (affectedRows > 0)
            {
                _logger.LogInformation("Proposal created successfully.");
                return Result<bool>.Success(true, "پیشنهاد شما با موفقیت ثبت شد.");
            }

            _logger.LogWarning("Failed to save proposal.");
            return Result<bool>.Failure("خطایی در ثبت پیشنهاد رخ داد.");
        }

        public async Task<Result<List<ExpertProposalDto>>> GetExpertProposals(int expertId, CancellationToken cancellationToken)
        {
           
            var proposals = await _proposalRepository.GetByExpertId(expertId, cancellationToken);

           
            if (proposals == null || !proposals.Any())
            {
                return Result<List<ExpertProposalDto>>.Failure("شما هنوز هیچ پیشنهادی برای سفارشات ثبت نکرده‌اید.");
            }

            return Result<List<ExpertProposalDto>>.Success(proposals);
        }

        public async Task<Result<List<ProposalSummaryDto>>> GetOrderProposals(int orderId, CancellationToken cancellationToken)
        {
            var proposals = await _proposalRepository.GetByOrderId(orderId, cancellationToken);

            if (proposals == null || !proposals.Any())
            {
                return Result<List<ProposalSummaryDto>>.Failure("هنوز پیشنهادی برای این سفارش ثبت نشده است.");
            }

            return Result<List<ProposalSummaryDto>>.Success(proposals);
        }

        public async Task<Result<bool>> ChangeStatus(int proposalId, int orderId, ProposalStatus newStatus, CancellationToken cancellationToken)
        {
            
            var isExist = await _orderRepository.IsExists(orderId, cancellationToken);
            if (!isExist) return Result<bool>.Failure("سفارشی با این شناسه موجود نیست.");

            var isProposalValid = await _proposalRepository.IsRelatedToOrder(proposalId, orderId, cancellationToken);
            if (!isProposalValid) return Result<bool>.Failure("این پیشنهاد متعلق به سفارش مورد نظر نمی‌باشد.");

            int affectedRowsOrder = 0;
            int affectedRowsProposal = 0;

            if (newStatus == ProposalStatus.Accepted)
            {
                
                var alreadyHasAccepted = await _proposalRepository.AnyAccepted(orderId, cancellationToken);
                if (alreadyHasAccepted) return Result<bool>.Failure("شما قبلاً یک متخصص را برای این سفارش تایید کرده‌اید.");

                
                affectedRowsProposal = await _proposalRepository.ChangeStatus(proposalId, newStatus, cancellationToken);

                
                await _proposalRepository.RejectOtherProposals(proposalId, orderId, cancellationToken);

                
                affectedRowsOrder = await _orderRepository.ChangeStatus(orderId, OrderStatus.Started, cancellationToken);

                if (affectedRowsProposal > 0 && affectedRowsOrder > 0)
                    return Result<bool>.Success(true, "پیشنهاد تایید شد، بقیه پیشنهادها رد شدند و کار آغاز گردید.");
            }
            else if (newStatus == ProposalStatus.Pending)
            {
               
                affectedRowsProposal = await _proposalRepository.ChangeStatus(proposalId, newStatus, cancellationToken);
                affectedRowsOrder = await _orderRepository.ChangeStatus(orderId, OrderStatus.WaitingForProposals, cancellationToken);

                if (affectedRowsProposal > 0 && affectedRowsOrder > 0)
                    return Result<bool>.Success(true, "تایید لغو شد و سفارش به حالت انتظار بازگشت.");
            }
            else if (newStatus == ProposalStatus.Rejected)
            {
                affectedRowsProposal = await _proposalRepository.ChangeStatus(proposalId, newStatus, cancellationToken);
                if (affectedRowsProposal > 0) return Result<bool>.Success(true, "پیشنهاد با موفقیت رد شد.");
            }

            return Result<bool>.Failure("خطا در به‌روزرسانی وضعیت.");
        }

        public async Task<bool> IsAlreadySubmitted(int expertId, int orderId, CancellationToken cancellationToken)
        {
           return await _proposalRepository.IsAlreadySubmitted(expertId, orderId, cancellationToken);
        }

        public async Task<Result<int>> GetExpertIdByOrderId(int orderId , CancellationToken cancellationToken) 
        {
           int expertId=await _proposalRepository.GetExpertIdByOrderId(orderId, cancellationToken);
            if (expertId<=0)
            {
                return Result<int>.Failure("خطایی در پیدا کردن کارشناس سفارش رخ داده است، دوباره تلاش کنید.");
            }

            return Result<int>.Success(expertId);
        }

        public async Task<Result<decimal>> GetPriceByOrderId(int orderId , CancellationToken cancellationToken) 
        {
            var price= await _proposalRepository.GetPriceByOrderId(orderId, cancellationToken);

            if (price<=0)
            {
                return Result<decimal>.Failure("برای این سفارش هنوز پیشنهادی تأیید نشده یا قیمت معتبری ثبت نگردیده است.");
            }

            return Result<decimal>.Success(price);
        }

    }
}

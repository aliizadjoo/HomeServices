using App.Domain.Core._common;
using App.Domain.Core.Contract.OrderAgg.Repository;
using App.Domain.Core.Contract.ProposalAgg.Repository;
using App.Domain.Core.Contract.ProposalAgg.Service;
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
        , IOrderRepository _orderRepository) : IProposalService
    {
        public async Task<Result<bool>> Create(ProposalCreateDto proposalCreateDto, CancellationToken cancellationToken)
        {
          
            _logger.LogInformation("Attempting to create a new proposal for OrderId: {OrderId} by ExpertId: {ExpertId}",
                proposalCreateDto.OrderId, proposalCreateDto.ExpertId);

        
            var affectedRows = await _proposalRepository.Create(proposalCreateDto, cancellationToken);

           
            if (affectedRows > 0)
            {
                _logger.LogInformation("Proposal successfully created for OrderId: {OrderId} with {Rows} affected row(s).",
                    proposalCreateDto.OrderId, affectedRows);

                return Result<bool>.Success(true, "پیشنهاد شما با موفقیت ثبت شد و برای مشتری ارسال گردید.");
            }

        
            _logger.LogWarning("Failed to save the proposal in the database for OrderId: {OrderId}.",
                proposalCreateDto.OrderId);

            return Result<bool>.Failure("خطایی در هنگام ذخیره پیشنهاد در سیستم رخ داد. لطفاً مجدداً تلاش کنید.");
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
            if (!isExist)
            {
                return Result<bool>.Failure("سفارشی با این شناسه موجود نیست.");
            }

            var isProposalValid = await _proposalRepository.IsRelatedToOrder(proposalId, orderId, cancellationToken);
            if (!isProposalValid)
            {
                return Result<bool>.Failure("این پیشنهاد متعلق به سفارش مورد نظر نمی‌باشد.");
            }

            int affectedRowsOrder = 0;
            int affectedRowsProposal = 0;

           
            if (newStatus == ProposalStatus.Accepted)
            {
               
                var alreadyHasAccepted = await _proposalRepository.AnyAccepted(orderId, cancellationToken);
                if (alreadyHasAccepted)
                {
                    return Result<bool>.Failure("شما قبلاً یک متخصص را برای این سفارش تایید کرده‌اید.");
                }

                affectedRowsProposal = await _proposalRepository.ChangeStatus(proposalId, newStatus, cancellationToken);
                affectedRowsOrder = await _orderRepository.ChangeStatus(orderId, OrderStatus.Started, cancellationToken);

                if (affectedRowsProposal > 0 && affectedRowsOrder > 0)
                    return Result<bool>.Success(true, "پیشنهاد تایید شد و کار آغاز گردید.");
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

                if (affectedRowsProposal > 0)
                    return Result<bool>.Success(true, "پیشنهاد با موفقیت رد شد.");
            }
            else
            {
                return Result<bool>.Failure("وضعیت ارسالی معتبر نیست.");
            }

            return Result<bool>.Failure("خطا در به‌روزرسانی وضعیت. عملیات انجام نشد.");
        }


    }
}

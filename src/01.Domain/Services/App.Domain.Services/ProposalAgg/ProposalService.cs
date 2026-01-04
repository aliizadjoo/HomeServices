using App.Domain.Core._common;
using App.Domain.Core.Contract.ProposalAgg.Repository;
using App.Domain.Core.Contract.ProposalAgg.Service;
using App.Domain.Core.Dtos.ProposalAgg;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.ProposalAgg
{
    public class ProposalService(IProposalRepository _proposalRepository , ILogger<ProposalService> _logger) : IProposalService
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
    }
}

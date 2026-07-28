using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Replacements;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.UpdateReplacement
{
    public class UpdateReplacementCommandHandler : IRequestHandler<UpdateReplacementCommand,
        UpdateReplacementResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IReplacementRepository _replacementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateReplacementCommandHandler(
            ICurrentUserService currentUserService, 
            IReplacementRepository replacementRepository, 
            IUnitOfWork unitOfWork,
            IQuizmanRepository quizmanRepository)
        {
            _currentUserService = currentUserService;
            _replacementRepository = replacementRepository;
            _unitOfWork = unitOfWork;
            _quizmanRepository = quizmanRepository;
        }

        public async Task<UpdateReplacementResponse> Handle(UpdateReplacementCommand request, 
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException("Пользователь не авторизован");

            var replacement = await _replacementRepository.GetByIdAsync(request.ReplacementId,
                cancellationToken);
            if (replacement == null)
                throw new NotFoundException("Замена не существует");

            var userId = _currentUserService.UserId ?? Guid.Empty;

            var quizman = await _quizmanRepository.GetByUserIdAsync(userId, cancellationToken);
            if (quizman != null && replacement.QuizemanId != quizman.Id)
                throw new ForbiddenException("Можно редактировать только свою замену");

            replacement.Update(request.Comment, request.IsFullShift);
            _replacementRepository.Update(replacement);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new UpdateReplacementResponse
            {
                ReplacementId = replacement.Id,
                Comment = replacement.Comment,
                IsFullShift = replacement.IsFullShift,
                UpdatedTime = DateTime.UtcNow
            };
        }
    }
}

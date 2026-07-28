using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Replacements;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.TakeReplacement
{
    public class TakeReplacementCommandHandler : IRequestHandler<TakeReplacementCommand,
        TakeReplacementResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReplacementRepository _replacementRepository;
        private readonly IAdminRepository _adminRepository;

        public TakeReplacementCommandHandler(ICurrentUserService currentUserService, 
            IUnitOfWork unitOfWork, 
            IReplacementRepository replacementRepository,
            IAdminRepository adminRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _replacementRepository = replacementRepository;
            _adminRepository = adminRepository;
        }

        public async Task<TakeReplacementResponse> Handle(TakeReplacementCommand request, 
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может взять квизмена с замены");

            var replacement = await _replacementRepository.GetByIdAsync(request.ReplacementId,
                cancellationToken);
            if (replacement == null)
                throw new NotFoundException("Квизмен не существует");

            var userId = _currentUserService.UserId ?? default;

            var admin = await _adminRepository.GetByUserIdAsync(userId); 

            replacement.Take(admin.Id);
            admin.TakeReplacement(replacement);

            _adminRepository.Update(admin);
            _replacementRepository.Update(replacement);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new TakeReplacementResponse
            {
                ReplacementId = replacement.Id,
                IsTaken = true,
                QuizmanId = replacement.QuizemanId,
                IsFullShift = replacement.IsFullShift,
                Comment = replacement.Comment
            };
        }
    }
}

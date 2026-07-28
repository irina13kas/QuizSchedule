using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Replacements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.DeleteReplacement
{
    public class DeleteReplacementCommandHandler : IRequestHandler<DeleteReplacementCommand, 
        ChangeStateReplacement>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IReplacementRepository _replacementRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReplacementCommandHandler(ICurrentUserService currentUserService,
            IQuizmanRepository quizmanRepository,
            IReplacementRepository replacementRepository, 
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _replacementRepository = replacementRepository;
            _quizmanRepository = quizmanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ChangeStateReplacement> Handle(DeleteReplacementCommand request,
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new ForbiddenException("Пользователь не аутентифицирован");

            var replacement = await _replacementRepository.GetByIdAsync(request.ReplacementId,
                cancellationToken);
            if (replacement == null)
                throw new NotFoundException("Замена не существует");

            var userId = _currentUserService.UserId ?? Guid.Empty;

            var quizman = await _quizmanRepository.GetByUserIdAsync(userId, cancellationToken);
            if (quizman != null && replacement.QuizemanId != quizman.Id)
                throw new ForbiddenException("Можно удалять только свою замену");

            _replacementRepository.Remove(replacement);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ChangeStateReplacement
            {
                Successed = true,
            };
        }
    }
}

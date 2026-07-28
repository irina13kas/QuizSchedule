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

namespace Application.Features.Replacements.AddReplacement
{
    public class AddReplacementCommandHandler : IRequestHandler<AddReplacementCommand,
        ReplacementResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReplacementRepository _replacementRepository;
        private readonly IQuizmanRepository _quizmanRepository;

        public AddReplacementCommandHandler(ICurrentUserService currentUserService, 
            IUnitOfWork unitOfWork, 
            IReplacementRepository replacementRepository,
            IQuizmanRepository quizmanRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _replacementRepository = replacementRepository;
            _quizmanRepository = quizmanRepository;
        }

        public async Task<ReplacementResponse> Handle(AddReplacementCommand request, 
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var quizman = await _quizmanRepository.GetByIdAsync(request.QuizmanId, 
                cancellationToken);
            if (quizman == null)
                throw new NotFoundException("Квизмена не существует");

            var existReplacement = await _replacementRepository.GetReplacementByDateAndQuizmanIdAsync(request.QuizmanId,
                request.Date, cancellationToken);

            if (existReplacement != null)
                throw new ExistInDBException("У этого пользователя уже есть замена на сегодня");

            var replacement = new Replacement(request.Date, quizman.Id, request.Comment, 
                request.IsFullShift);

            await _replacementRepository.AddAsync(replacement, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ReplacementResponse
            {
                Id = replacement.Id,
                Date = replacement.Date,
                Comment = replacement.Comment,
                IsFullShift = replacement.IsFullShift,
                QuizemanId = replacement.QuizemanId,
                QuizemanName = quizman.User.Name,
                Status = replacement.Status.ToString(),
                CreatedAt = replacement.CreatedAt
            };
        }
    }
}

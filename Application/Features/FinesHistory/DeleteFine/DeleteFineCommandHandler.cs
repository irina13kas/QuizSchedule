using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Fines;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinesHistory.DeleteFine
{
    public class DeleteFineCommandHandler : IRequestHandler<DeleteFineCommand, FineChangeStateResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Fine> _fineRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteFineCommandHandler(
            ICurrentUserService currentUserService,
            IRepository<Fine> fineRepository, 
            IQuizmanRepository quizmanRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _fineRepository = fineRepository;
            _quizmanRepository = quizmanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FineChangeStateResponse> Handle(DeleteFineCommand request, CancellationToken cancellationToken)
        {
            if(_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может закрывать штрафы");

            var fine = await _fineRepository.GetByIdAsync(request.Id);
            if (fine == null)
                throw new NotFoundException("Не существует штрафа с заданным Id");

            var quizman = await _quizmanRepository.GetByIdAsync(fine.QuizmanId);
            quizman.RemoveFine(fine);

            _fineRepository.Remove(fine);
            await _unitOfWork.SaveChangesAsync();

            return new FineChangeStateResponse
            {
                Success = true
            };
        }
    }
}

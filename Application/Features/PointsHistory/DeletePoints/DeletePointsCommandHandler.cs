using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Points;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PointsHistory.DeletePoints
{
    public class DeletePointsCommandHandler : IRequestHandler<DeleteSmartCommand, PointsChangeStateResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IPointsRepository _pointsRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePointsCommandHandler(ICurrentUserService currentUserService, 
            IPointsRepository pointsRepository, IQuizmanRepository quizmanRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _pointsRepository = pointsRepository;
            _quizmanRepository = quizmanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PointsChangeStateResponse> Handle(DeleteSmartCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role == UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может удалять записи о баллах");

            var points = await _pointsRepository.GetByIdAsync(request.PointsId, cancellationToken);
            if (points == null)
                throw new NotFoundException("Баллы не найдены");

            var quizman = await _quizmanRepository.GetByIdAsync(points.QuizmanId, cancellationToken);
            if (quizman == null)
                throw new NotFoundException("Квизмен не существует");

            quizman.RemovePoints(points);
            _pointsRepository.Remove(points);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new PointsChangeStateResponse
            {
                Success = true,
            };
        }
    }
}

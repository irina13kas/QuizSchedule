using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Fines;
using Application.Features.FinesHistory.AddFine;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinesHistory.UpdateFine
{
    public class UpdateFineCommandHandler : IRequestHandler<UpdateFineCommand, UpdateFineResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Fine> _finesRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IRepository<Game> _gamesRepository;
        private readonly IGameParticipantRepository _participantRepository;

        public UpdateFineCommandHandler(
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork, 
            IRepository<Fine> finesRepository, 
            IRepository<Game> gamesRepository, 
            IQuizmanRepository quizmanRepository, 
            IAdminRepository adminRepository,
            IGameParticipantRepository participantRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _finesRepository = finesRepository;
            _gamesRepository = gamesRepository;
            _quizmanRepository = quizmanRepository;
            _adminRepository = adminRepository;
            _participantRepository = participantRepository;
        }

        public async Task<UpdateFineResponse> Handle(UpdateFineCommand request, CancellationToken cancellation)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может обновлять информацию о штрафе");

            var fine = await _finesRepository.GetByIdAsync(request.Id, cancellation);

            if (fine == null)
                throw new NotFoundException("Штрафа не существует");

            var quizeman = await _quizmanRepository.GetByIdAsync(request.QuizmanId, cancellation);

            if (quizeman == null || quizeman.User.Role == UserRole.Quizman || quizeman.User.IsDeleted)
                throw new NotFoundException("Квизмена не существует");

            var admin = await _adminRepository.GetByIdAsync(request.AdminId, cancellation);

            if (admin == null || admin.User.Role == UserRole.Admin || admin.User.IsDeleted)
                throw new NotFoundException("Админ не существует");

            string gameName = string.Empty;

            if (request.GameId.HasValue)
            {

                var game = await _gamesRepository.GetByIdAsync(request.GameId.Value);


                if (game == null || game.Status != GameStatus.Available)
                    throw new NotFoundException("Игры не существует");

                if (!(await _participantRepository
                    .IsQuizmanTakesPlaceOnGame(quizeman.Id, game.Id, cancellation)))
                    throw new NotBusinessSuitableException("Нельзя выдавать штраф квизмену за игру, где он не присутствовал");

                gameName = game.Name;
            }

            fine.Update(request.QuizmanId, request.AdminId, request.Amount, request.Comment, request.GameId);
            _finesRepository.Update(fine);

            await _unitOfWork.SaveChangesAsync(cancellation);

            return new UpdateFineResponse
            {
                Id = fine.Id,
                QuizmanId = fine.QuizmanId,
                QuizmanName = quizeman.User.Name,
                AdminId = fine.AdminId,
                AdminName = admin.User.Name,
                Amount = fine.Amount,
                Comment = fine.Comment,
                GameId = fine.GameId.Value,
                GameName = gameName,
                UpdatedAt = fine.CreatedAt
            };

        }

    }
}

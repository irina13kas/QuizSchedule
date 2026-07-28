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

namespace Application.Features.FinesHistory.AddFine
{
    public class AddFineCommandHandler: IRequestHandler<AddFineCommand, AddFineResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Fine> _finesRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IQuizmanRepository _quizmanRepository;
        private readonly IRepository<Game> _gamesRepository;
        private readonly IGameParticipantRepository _participantRepository;

        public AddFineCommandHandler(
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork, 
            IRepository<Fine> finesRepository, 
            IRepository<Game> gamesRepository, 
            IAdminRepository adminRepository, 
            IQuizmanRepository quizmanRepository,
            IGameParticipantRepository participantRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _finesRepository = finesRepository;
            _gamesRepository = gamesRepository;
            _adminRepository = adminRepository;
            _quizmanRepository = quizmanRepository;
            _participantRepository = participantRepository;
        }

        public async Task<AddFineResponse> Handle(AddFineCommand request, CancellationToken cancellation)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может начислять штраф");

            var quizeman = await _quizmanRepository.GetByIdAsync(request.QuizmanId);

            if (quizeman == null || quizeman.User.Role == UserRole.Quizman || quizeman.User.IsDeleted)
                throw new NotFoundException("Квизмена не существует");

            var admin = await _adminRepository.GetByIdAsync(request.AdminId);

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

            var fine = new Fine(request.QuizmanId, request.AdminId, request.Amount, 
                request.Comment, request.GameId);

            quizeman.AddFine(fine);

            await _finesRepository.AddAsync(fine);
            await _unitOfWork.SaveChangesAsync();

            return new AddFineResponse
            {
                Id = fine.Id,
                QuizmanId = fine.QuizmanId,
                QuizmanName = quizeman.User.Name,
                Amount = fine.Amount,
                Comment = fine.Comment,
                GameId = fine.GameId.Value,
                GameName = gameName,
                AdminId = admin.Id,
                AdminName = admin.User.Name,
                CreatedAt = fine.CreatedAt,
                IsClosed = fine.IsClosed
            };

        }
    }
}

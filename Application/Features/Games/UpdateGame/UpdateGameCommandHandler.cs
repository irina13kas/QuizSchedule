using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Games;
using Application.Features.Games.CreateGame;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.UpdateGame
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, GameResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IGameRepository _gameRepository;
        private readonly IRepository<Bar> _barRepository;
        private readonly IRepository<Master> _masterRepository;
        private readonly IRepository<Dj> _djRepository;
        private readonly IRepository<Photographer> _photographerRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateGameCommandHandler(ICurrentUserService currentUserService,
            IGameRepository gameRepository, IRepository<Bar> barRepository,
            IRepository<Master> masterRepository, IRepository<Dj> djRepository,
            IRepository<Photographer> photographerRepository, IAdminRepository adminRepository,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _gameRepository = gameRepository;
            _barRepository = barRepository;
            _masterRepository = masterRepository;
            _djRepository = djRepository;
            _photographerRepository = photographerRepository;
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GameResponse> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role == UserRole.Admin.ToString())
                throw new ForbiddenException("Создавать Игру может только Администратор");

            var game = await _gameRepository.GetByIdAsync(request.GameId, cancellationToken);

            if(game == null)
                throw new NotFoundException("Игра не существует");

            var existGame = await _gameRepository.GetByNameAsync(request.Name, cancellationToken);
            if (existGame != null)
                throw new ExistInDBException("Игра с таким именем уже присутствует в БД");

            if (request.BarId != Guid.Empty)
            {
                var bar = await _barRepository.GetByIdAsync(request.BarId, cancellationToken);
                if (bar == null)
                    throw new NotFoundException("Бар не существует");
            }

            if (request.MasterId != Guid.Empty)
            {
                var master = await _masterRepository.GetByIdAsync(request.MasterId, cancellationToken);
                if (master == null)
                    throw new NotFoundException("Ведущий не существует");
            }

            if (request.DjId != Guid.Empty)
            {
                var dj = await _djRepository.GetByIdAsync(request.DjId, cancellationToken);
                if (dj == null)
                    throw new NotFoundException("Dj не существует");
            }

            if (request.PhotographerId != Guid.Empty)
            {
                var photographer = await _photographerRepository.GetByIdAsync(request.PhotographerId, cancellationToken);
                if (photographer == null)
                    throw new NotFoundException("Фотограф не существует");
            }

            if (request.ResponsibleAdminId != Guid.Empty)
            {
                var admin = await _adminRepository.GetByIdAsync(request.ResponsibleAdminId, cancellationToken);
                if (admin == null)
                    throw new NotFoundException("Администратор не существует");
            }

            game.Update(request.Name, Enum.TryParse<GameStatus>(request.Status, ignoreCase: true, out GameStatus role) ? role: GameStatus.Draft, request.ResponsibleAdminId, request.BarId,
                request.MasterId, request.DjId, request.PhotographerId, request.GameStartTime,
                request.WorkStartTime, request.Partner ?? "-");

            _gameRepository.Update(game);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GameResponse>(game);
        }
    }
}

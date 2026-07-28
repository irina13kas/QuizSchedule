using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Games;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, GameResponse>
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

        public CreateGameCommandHandler(ICurrentUserService currentUserService,
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

        public async Task<GameResponse> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            Bar bar = null;
            Admin admin = null;
            Master master = null;
            Dj dj = null;
            Photographer photographer = null;

            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Создавать Игру может только Администратор");

            var existGame = await _gameRepository.GetByNameAsync(request.Name, cancellationToken);
            if (existGame != null)
                throw new ExistInDBException("Игра с таким именем уже присутствует в БД");

            if (request.BarId != null)
            {
                bar = await _barRepository.GetByIdAsync(request.BarId, cancellationToken);
                if (bar == null)
                    throw new NotFoundException("Бар не существует");
                if (await _gameRepository.IsBarBuzyAsync(bar.Id, request.GameStartTime, cancellationToken))
                    throw new NotBusinessSuitableException("Бар занят на это время");
            }

            if (request.MasterId != null)
            {
                master = await _masterRepository.GetByIdAsync(request.MasterId, cancellationToken);
                if (master == null)
                    throw new NotFoundException("Ведущий не существует");
                if (await _gameRepository.IsMasterBuzyAsync(master.Id, request.GameStartTime, cancellationToken))
                    throw new NotBusinessSuitableException("Ведущий занят на это время");
            }

            if (request.DjId != null)
            {
                dj = await _djRepository.GetByIdAsync(request.DjId, cancellationToken);
                if (dj == null)
                    throw new NotFoundException("Dj не существует");
                if (await _gameRepository.IsDjBuzyAsync(dj.Id, request.GameStartTime, cancellationToken))
                    throw new NotBusinessSuitableException("Dj занят на это время");
            }

            if (request.PhotographerId != null)
            {
                photographer = await _photographerRepository.GetByIdAsync(request.PhotographerId, cancellationToken);
                if (photographer == null)
                    throw new NotFoundException("Фотограф не существует");
                if (await _gameRepository.IsPhotographerBuzyAsync(photographer.Id, request.GameStartTime, cancellationToken))
                    throw new NotBusinessSuitableException("Фотограф занят на это время");
            }

            if (request.ResponsibleAdminId != null)
            {
                admin = await _adminRepository.GetByIdAsync(request.ResponsibleAdminId, cancellationToken);
                if (admin == null)
                    throw new NotFoundException("Администратор не существует");
                if (await _gameRepository.IsAdminBuzyAsync(admin.Id, request.GameStartTime, cancellationToken))
                    throw new NotBusinessSuitableException("Админ занят на это время");
            }

            var game = new Game(
                request.Name, 
                _currentUserService.UserId.Value, 
                bar?.Id ?? Guid.Empty,
                request.Partner ?? "-", 
                admin?.Id ?? Guid.Empty, 
                request.GameStartTime, 
                request.WorkStartTime, 
                master?.Id ?? Guid.Empty, 
                dj?.Id ?? Guid.Empty, 
                photographer?.Id ?? Guid.Empty);

            await _gameRepository.AddAsync(game, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<GameResponse>(game);
        }
    }
}

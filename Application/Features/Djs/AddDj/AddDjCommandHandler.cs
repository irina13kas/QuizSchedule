using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Bars;
using Application.DTOs.Djs;
using Application.Features.Bars.AddBar;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Djs.AddDj
{
    public class AddDjCommandHandler : IRequestHandler<AddDjCommand, DjResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Dj> _djsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddDjCommandHandler(
            ICurrentUserService currentUserService, 
            IRepository<Dj> djsRepository, 
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _djsRepository = djsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DjResponse> Handle(AddDjCommand request, 
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может создавать новых Dj");

            var dj = new Dj(request.Name);

            await _djsRepository.AddAsync(dj, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DjResponse
            {
                DjId = dj.Id,
                Name = dj.Name,
                IsActive = dj.IsActive,
            };
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Bars;
using Application.DTOs.Djs;
using Application.Features.Bars.UpdateBar;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Djs.UpdateDj
{
    public class UpdateDjCommandHandler : IRequestHandler<UpdateDjCommand, DjResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Dj> _djsRepository;

        public UpdateDjCommandHandler(
            ICurrentUserService currentUserService, 
            IUnitOfWork unitOfWork, 
            IRepository<Dj> djsRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _djsRepository = djsRepository;
        }

        public async Task<DjResponse> Handle(UpdateDjCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может менять информацию о Dj");

            var dj = await _djsRepository.GetByIdAsync(request.DjId, cancellationToken);
            if (dj == null || !dj.IsActive)
                throw new NotFoundException("Dj не существует");

            dj.UpdateInfo(request.NewName);
            _djsRepository.Update(dj);

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

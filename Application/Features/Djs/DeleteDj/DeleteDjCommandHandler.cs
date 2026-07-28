using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Bars;
using Application.DTOs.Djs;
using Application.Features.Bars.DeleteBar;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Djs.DeleteDj
{
    public class DeleteDjCommandHandler : IRequestHandler<DeleteDjCommand, 
        ChangeStateDjResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Dj> _djsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDjCommandHandler(
            ICurrentUserService currentUserService, 
            IRepository<Dj> djsRepository, 
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _djsRepository = djsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ChangeStateDjResponse> Handle(DeleteDjCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может удалять Dj");

            var dj = await _djsRepository.GetByIdAsync(request.DjId, cancellationToken);
            if (dj == null)
                throw new NotFoundException("Dj не существует");

            dj.ChangeActivity();
            _djsRepository.Update(dj);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ChangeStateDjResponse
            {
                Success = true
            };
        }
    }
}

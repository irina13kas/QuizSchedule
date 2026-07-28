using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Djs;
using Application.DTOs.Masters;
using Application.Features.Djs.UpdateDj;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Masters.UpdateMaster
{
    public class UpdateMasterCommandHandler : IRequestHandler<UpdateMasterCommand,
        MasterResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Master> _mastersRepository;

        public UpdateMasterCommandHandler(
            ICurrentUserService currentUserService, 
            IUnitOfWork unitOfWork, 
            IRepository<Master> mastersRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _mastersRepository = mastersRepository;
        }

        public async Task<MasterResponse> Handle(UpdateMasterCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может менять информацию о Ведущем");

            var master = await _mastersRepository.GetByIdAsync(request.MasterId, cancellationToken);
            if (master == null || !master.IsActive)
                throw new NotFoundException("Ведущего не существует");

            master.UpdateInfo(request.NewName);
            _mastersRepository.Update(master);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new MasterResponse
            {
                MasterId = master.Id,
                Name = master.Name,
                IsActive = master.IsActive,
            };
        }
    }
}

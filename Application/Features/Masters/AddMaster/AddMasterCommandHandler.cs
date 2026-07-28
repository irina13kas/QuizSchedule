using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Djs;
using Application.DTOs.Masters;
using Application.Features.Djs.AddDj;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Masters.AddMaster
{
    public class AddMasterCommandHandler : IRequestHandler<AddMasterCommand, MasterResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Master> _mastersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddMasterCommandHandler(
            ICurrentUserService currentUserService, 
            IRepository<Master> mastersRepository, 
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _mastersRepository = mastersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<MasterResponse> Handle(AddMasterCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может создавать новых Ведущих");

            var master = new Master(request.Name);

            await _mastersRepository.AddAsync(master, cancellationToken);
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

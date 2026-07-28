using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Auth;
using Application.DTOs.Fines;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinesHistory.CloseFine
{
    public class CloseFineCommandHandler : IRequestHandler<CloseFineCommand, FineChangeStateResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IFineRepository _fineRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CloseFineCommandHandler(ICurrentUserService currentUserService, IFineRepository fineRepository, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _fineRepository = fineRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FineChangeStateResponse> Handle(CloseFineCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Пользователь не имеет прав Администратора");

            var fine = await _fineRepository.GetByIdAsync(request.FineId);
            if (fine == null)
                throw new NotFoundException("Штраф не найден");

            fine.ChangeStatus();

            await _unitOfWork.SaveChangesAsync();

            return new FineChangeStateResponse
            {
                Success = true
            };
        }
    }
}

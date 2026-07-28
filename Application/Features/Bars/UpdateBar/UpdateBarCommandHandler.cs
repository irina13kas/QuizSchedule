using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Bars;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bars.UpdateBar
{
    public class UpdateBarCommandHandler : IRequestHandler<UpdateBarCommand, BarResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Bar> _barsRepository;

        public UpdateBarCommandHandler(
            ICurrentUserService currentUserService, 
            IUnitOfWork unitOfWork, 
            IRepository<Bar> barsRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _barsRepository = barsRepository;
        }

        public async Task<BarResponse> Handle(UpdateBarCommand request, 
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может менять информацию о баре");

            var bar = await _barsRepository.GetByIdAsync(request.BarId, cancellationToken);
            if (bar == null || !bar.IsActive)
                throw new NotFoundException("Бар не существует");

            bar.UpdateInfo(request.NewBarName, request.NewAddress, request.NewCapacity);
            _barsRepository.Update(bar);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BarResponse
            {
                BarId = bar.Id,
                BarName = bar.Name,
                Address = bar.Address,
                Capacity = bar.Capacity,
                IsActive = bar.IsActive,
            };
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Bars;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bars.AddBar
{
    public class AddBarCommandHandler : IRequestHandler<AddBarCommand, BarResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Bar> _barsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddBarCommandHandler(
            ICurrentUserService currentUserService, 
            IRepository<Bar> barsRepository, 
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _barsRepository = barsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BarResponse> Handle(AddBarCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может создавать новые бары");

            var bar = new Bar(request.BarName, request.Address, request.Capacity);

            await _barsRepository.AddAsync(bar, cancellationToken);
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

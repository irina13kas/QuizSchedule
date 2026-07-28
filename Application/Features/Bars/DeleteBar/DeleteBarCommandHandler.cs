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

namespace Application.Features.Bars.DeleteBar
{
    public class DeleteBarCommandHandler : IRequestHandler<DeleteBarCommand, 
        ChangeStateBarResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Bar> _barsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBarCommandHandler(
            ICurrentUserService currentUserService, 
            IRepository<Bar> barsRepository, 
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _barsRepository = barsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ChangeStateBarResponse> Handle(DeleteBarCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может удалять бары");

            var bar = await _barsRepository.GetByIdAsync(request.BarId, cancellationToken);
            if (bar == null)
                throw new NotFoundException("Бар не существует");

            bar.ChangeActivity();
            _barsRepository.Update(bar);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ChangeStateBarResponse
            {
                Success = true
            };
        }
    }
}

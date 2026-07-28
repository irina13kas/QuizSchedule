using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Masters;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Masters.DeleteMaster
{
    public class DeleteMasterCommandHandler : IRequestHandler<DeleteMasterCommand, 
        ChangeStateMasterResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Master> _mastersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMasterCommandHandler(
            ICurrentUserService currentUserService, 
            IRepository<Master> mastersRepository, 
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _mastersRepository = mastersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ChangeStateMasterResponse> Handle(DeleteMasterCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new ForbiddenException("Только Админ может удалять Ведущего");

            var master = await _mastersRepository.GetByIdAsync(request.MasterId, cancellationToken);
            if (master == null)
                throw new NotFoundException("Ведущий не существует");

            master.ChangeActivity();
            _mastersRepository.Update(master);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ChangeStateMasterResponse
            {
                Success = true
            };
        }
    }
}

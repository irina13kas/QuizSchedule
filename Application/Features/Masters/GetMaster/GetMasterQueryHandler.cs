using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Djs;
using Application.DTOs.Masters;
using Application.Features.Djs.GetDj;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Masters.GetMaster
{
    public class GetMasterQueryHandler : IRequestHandler<GetMasterQuery, MasterResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Master> _mastersRepository;

        public GetMasterQueryHandler(
            ICurrentUserService currentUserService, 
            IRepository<Master> mastersRepository)
        {
            _currentUserService = currentUserService;
            _mastersRepository = mastersRepository;
        }

        public async Task<MasterResponse> Handle(GetMasterQuery query, 
            CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAuthenticated)
                throw new UnauthorizedException();

            var master = await _mastersRepository.GetByIdAsync(query.MasterId, cancellationToken);
            if (master == null || !master.IsActive)
                throw new NotFoundException("Ведущий не существует");

            return new MasterResponse
            {
                MasterId = master.Id,
                Name = master.Name,
                IsActive = master.IsActive,
            };
        }
    }
}

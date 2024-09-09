using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Suspends.Queries.GetList;

public class GetListSuspendQuery : IRequest<GetListResponse<GetListSuspendListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListSuspendQueryHandler : IRequestHandler<GetListSuspendQuery, GetListResponse<GetListSuspendListItemDto>>
    {
        private readonly ISuspendRepository _suspendRepository;
        private readonly IMapper _mapper;

        public GetListSuspendQueryHandler(ISuspendRepository suspendRepository, IMapper mapper)
        {
            _suspendRepository = suspendRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSuspendListItemDto>> Handle(GetListSuspendQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Suspend> suspends = await _suspendRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSuspendListItemDto> response = _mapper.Map<GetListResponse<GetListSuspendListItemDto>>(suspends);
            return response;
        }
    }
}
using Amazon.Runtime.Internal;
using Application.Features.Retentions.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Retentions.Queries.GetListByUserId;
public class GetListByUserIdRetentionQuery: IRequest<ICollection<GetListByUserIdRetentionListItemDto>>, ISecuredRequest
{
    public Guid UserId { get; set; }

    public string[] Roles => [RetentionsOperationClaims.Admin, RetentionsOperationClaims.Read];

    public class GetListByUserIdRetentionQueryHandler: IRequestHandler<GetListByUserIdRetentionQuery, ICollection<GetListByUserIdRetentionListItemDto>>
    {
        private readonly IRetentionRepository _retentionRepository;
        private IMapper _mapper;

        public GetListByUserIdRetentionQueryHandler(IRetentionRepository retentionRepository, IMapper mapper)
        {
            _retentionRepository = retentionRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetListByUserIdRetentionListItemDto>> Handle(GetListByUserIdRetentionQuery request, CancellationToken cancellationToken)
        {
            ICollection<Retention> retentions = await _retentionRepository.GetAllListAsync(r => r.UserId == request.UserId);

            ICollection<GetListByUserIdRetentionListItemDto> response = _mapper.Map<ICollection<GetListByUserIdRetentionListItemDto>>(retentions);
            return response;
        }
    }
}

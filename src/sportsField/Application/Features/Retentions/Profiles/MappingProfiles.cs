using Application.Features.Retentions.Commands.Create;
using Application.Features.Retentions.Commands.Delete;
using Application.Features.Retentions.Commands.Update;
using Application.Features.Retentions.Queries.GetById;
using Application.Features.Retentions.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.CourtReservations.Queries.GetListByUserId;
using Application.Features.Retentions.Queries.GetListByUserId;

namespace Application.Features.Retentions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateRetentionCommand, Retention>();
        CreateMap<Retention, CreatedRetentionResponse>();

        CreateMap<UpdateRetentionCommand, Retention>();
        CreateMap<Retention, UpdatedRetentionResponse>();

        CreateMap<DeleteRetentionCommand, Retention>();
        CreateMap<Retention, DeletedRetentionResponse>();

        CreateMap<Retention, GetByIdRetentionResponse>();

        CreateMap<Retention, GetListRetentionListItemDto>();
        CreateMap<IPaginate<Retention>, GetListResponse<GetListRetentionListItemDto>>();

        CreateMap<Retention, GetListByUserIdRetentionListItemDto>();
    }
}
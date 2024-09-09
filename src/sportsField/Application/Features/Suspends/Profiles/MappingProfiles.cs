using Application.Features.Suspends.Commands.Create;
using Application.Features.Suspends.Commands.Delete;
using Application.Features.Suspends.Commands.Update;
using Application.Features.Suspends.Queries.GetById;
using Application.Features.Suspends.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Suspends.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateSuspendCommand, Suspend>();
        CreateMap<Suspend, CreatedSuspendResponse>();

        CreateMap<UpdateSuspendCommand, Suspend>();
        CreateMap<Suspend, UpdatedSuspendResponse>();

        CreateMap<DeleteSuspendCommand, Suspend>();
        CreateMap<Suspend, DeletedSuspendResponse>();

        CreateMap<Suspend, GetByIdSuspendResponse>();

        CreateMap<Suspend, GetListSuspendListItemDto>();
        CreateMap<IPaginate<Suspend>, GetListResponse<GetListSuspendListItemDto>>();
    }
}
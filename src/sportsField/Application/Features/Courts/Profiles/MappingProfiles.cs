using Application.Features.Courts.Commands.Create;
using Application.Features.Courts.Commands.Delete;
using Application.Features.Courts.Commands.Update;
using Application.Features.Courts.Queries.GetById;
using Application.Features.Courts.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Courts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateCourtCommand, Court>();
        CreateMap<Court, CreatedCourtResponse>();

        CreateMap<UpdateCourtCommand, Court>();
        CreateMap<Court, UpdatedCourtResponse>();

        CreateMap<DeleteCourtCommand, Court>();
        CreateMap<Court, DeletedCourtResponse>();

        CreateMap<Court, GetByIdCourtResponse>();

        CreateMap<Court, GetListCourtListItemDto>();
        CreateMap<IPaginate<Court>, GetListResponse<GetListCourtListItemDto>>();
    }
}
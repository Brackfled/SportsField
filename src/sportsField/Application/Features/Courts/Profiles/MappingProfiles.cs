using Application.Features.Courts.Commands.Create;
using Application.Features.Courts.Commands.Delete;
using Application.Features.Courts.Commands.Update;
using Application.Features.Courts.Queries.GetById;
using Application.Features.Courts.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.Courts.Commands.UpdateActivity;
using Application.Features.Courts.Queries.GetListByUserId;

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

        CreateMap<Court, GetByIdCourtResponse>()
            .ForMember(c => c.Attiributes, memberOptions: opt => opt.MapFrom(c => c.Attiributes))
            .ForMember(c => c.CourtImages, memberOptions: opt => opt.MapFrom(c => c.CourtImages))
            ;

        CreateMap<Court, GetListCourtListItemDto>()
            .ForMember(c => c.Attiributes, memberOptions: opt => opt.MapFrom(c => c.Attiributes))
            .ForMember(c => c.CourtImages, memberOptions: opt => opt.MapFrom(c => c.CourtImages))
            ;
        CreateMap<IPaginate<Court>, GetListResponse<GetListCourtListItemDto>>();

        CreateMap<Court, UpdatedActivityCourtResponse>();

        CreateMap<Court, GetListByUserIdCourtListItemDto>()
            .ForMember(c => c.Attiributes, memberOptions: opt => opt.MapFrom(c => c.Attiributes))
            .ForMember(c => c.CourtImages, memberOptions: opt => opt.MapFrom(c => c.CourtImages))
            ;
        CreateMap<IPaginate<Court>, GetListResponse<GetListByUserIdCourtListItemDto>>();
    }
}
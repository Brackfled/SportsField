using Application.Features.Attiributes.Commands.Create;
using Application.Features.Attiributes.Commands.Delete;
using Application.Features.Attiributes.Commands.Update;
using Application.Features.Attiributes.Queries.GetById;
using Application.Features.Attiributes.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.Attiributes.Queries.GetListByDynamic;

namespace Application.Features.Attiributes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateAttiributeCommand, Attiribute>();
        CreateMap<Attiribute, CreatedAttiributeResponse>();

        CreateMap<UpdateAttiributeCommand, Attiribute>();
        CreateMap<Attiribute, UpdatedAttiributeResponse>();

        CreateMap<DeleteAttiributeCommand, Attiribute>();
        CreateMap<Attiribute, DeletedAttiributeResponse>();

        CreateMap<Attiribute, GetByIdAttiributeResponse>();

        CreateMap<Attiribute, GetListAttiributeListItemDto>();
        CreateMap<IPaginate<Attiribute>, GetListResponse<GetListAttiributeListItemDto>>();

        CreateMap<Attiribute, GetListByDynamicAttiributeListItemDto>()
           .ForMember(a => a.Courts, memberOptions: opt => opt.MapFrom(a => a.Courts!))
           ;
        CreateMap<IPaginate<Attiribute>, GetListResponse<GetListByDynamicAttiributeListItemDto>>();
    }
}
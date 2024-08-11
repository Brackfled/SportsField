using Application.Features.CourtReservations.Commands.Create;
using Application.Features.CourtReservations.Commands.Delete;
using Application.Features.CourtReservations.Commands.Update;
using Application.Features.CourtReservations.Queries.GetById;
using Application.Features.CourtReservations.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.CourtReservations.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateCourtReservationCommand, CourtReservation>();
        CreateMap<CourtReservation, CreatedCourtReservationResponse>();

        CreateMap<UpdateCourtReservationCommand, CourtReservation>();
        CreateMap<CourtReservation, UpdatedCourtReservationResponse>();

        CreateMap<DeleteCourtReservationCommand, CourtReservation>();
        CreateMap<CourtReservation, DeletedCourtReservationResponse>();

        CreateMap<CourtReservation, GetByIdCourtReservationResponse>();

        CreateMap<CourtReservation, GetListCourtReservationListItemDto>();
        CreateMap<IPaginate<CourtReservation>, GetListResponse<GetListCourtReservationListItemDto>>();
    }
}
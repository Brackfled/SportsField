using Application.Features.CourtReservations.Commands.Create;
using Application.Features.CourtReservations.Commands.Delete;
using Application.Features.CourtReservations.Commands.Update;
using Application.Features.CourtReservations.Queries.GetById;
using Application.Features.CourtReservations.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.CourtReservations.Commands.QuickCreate;
using Application.Features.CourtReservations.Commands.UpdateActivity;
using Application.Features.CourtReservations.Commands.RentReservation;
using Application.Features.CourtReservations.Queries.GetListByUserId;
using Application.Features.CourtReservations.Commands.CancelReservation;
using Application.Features.CourtReservations.Queries.GetListByDynamic;
using Application.Features.CourtReservations.Queries.GetListByCourtUserId;

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

        CreateMap<CourtReservation, QuickCreatedCourtReservationResponse>();

        CreateMap<CourtReservation, UpdatedActivityCourtReservationResponse>();

        CreateMap<CourtReservation, RentReservationResponse>();

        CreateMap<CourtReservation, GetListByUserIdCourtReservationListItemDto>()
            .ForMember(cr => cr.CourtName, memberOptions: opt => opt.MapFrom(cr => cr.Court!.Name))
            .ForMember(cr => cr.CourtDescription, memberOptions: opt => opt.MapFrom(cr => cr.Court!.Description))
            .ForMember(cr => cr.CourtLat, memberOptions: opt => opt.MapFrom(cr => cr.Court!.Lat))
            .ForMember(cr => cr.CourtLng, memberOptions: opt => opt.MapFrom(cr => cr.Court!.Lng))
            .ForMember(cr => cr.CourtFormattedAddress, memberOptions: opt => opt.MapFrom(cr => cr.Court!.FormattedAddress))
            .ForMember(cr => cr.CourtPhoneNumber, memberOptions: opt => opt.MapFrom(cr => cr.Court!.PhoneNumber))
            ;
        CreateMap<IPaginate<CourtReservation>, GetListResponse<GetListByUserIdCourtReservationListItemDto>>();

        CreateMap<CourtReservation, CancelledReservationResponse>();

        CreateMap<CourtReservation, GetListByDynamicCourtReservationListItemDto>();
        CreateMap<IPaginate<CourtReservation>, GetListResponse<GetListByDynamicCourtReservationListItemDto>>();

        CreateMap<CourtReservation, GetListByCourtUserIdCourtReservationListItemDto>()
            .ForMember(cr => cr.CourtName, memberOptions: opt => opt.MapFrom(cr => cr.Court!.Name))
            .ForMember(cr => cr.UserFirstName, memberOptions: opt => opt.MapFrom(cr => cr.User!.FirstName))
            .ForMember(cr => cr.UserLastName, memberOptions: opt => opt.MapFrom(cr => cr.User!.LastName))
            .ForMember(cr => cr.UserEmail, memberOptions: opt => opt.MapFrom(cr => cr.User!.Email))
            ;
        CreateMap<IPaginate<CourtReservation>, GetListResponse<GetListByCourtUserIdCourtReservationListItemDto>>();
    }
}
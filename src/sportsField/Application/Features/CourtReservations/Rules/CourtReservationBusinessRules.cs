using Application.Features.CourtReservations.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.CourtReservations.Rules;

public class CourtReservationBusinessRules : BaseBusinessRules
{
    private readonly ICourtReservationRepository _courtReservationRepository;
    private readonly ILocalizationService _localizationService;

    public CourtReservationBusinessRules(ICourtReservationRepository courtReservationRepository, ILocalizationService localizationService)
    {
        _courtReservationRepository = courtReservationRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CourtReservationsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CourtReservationShouldExistWhenSelected(CourtReservation? courtReservation)
    {
        if (courtReservation == null)
            await throwBusinessException(CourtReservationsBusinessMessages.CourtReservationNotExists);
    }

    public async Task CourtReservationIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(
            predicate: cr => cr.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CourtReservationShouldExistWhenSelected(courtReservation);
    }
}
using Application.Services.CourtReservations;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters.BackgroundWorkers.Hangfire;
public class HangfireJobs
{
    private readonly ICourtReservationService _courtReservationService;

    public HangfireJobs(ICourtReservationService reservationService)
    {
        _courtReservationService = reservationService;
    }

    public async Task DeleteOldReservations()
    {
        await _courtReservationService.DeleteOldReservationsAsync();
    }
}

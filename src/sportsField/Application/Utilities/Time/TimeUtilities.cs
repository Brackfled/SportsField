using Application.Services.Retentions;
using Domain.Dtos;
using Domain.Entities;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities.Time;
public class TimeUtilities
{
    public static IList<DateTime> ConvertDateTimeListAsync(RetentionCommandDto retentionCommandDto)
    {
        IList<DateTime> dates = new List<DateTime>();
        foreach (string day in retentionCommandDto.ReservationDays)
        {
            CultureInfo trCulture = new CultureInfo("tr-TR");
            if (!trCulture.DateTimeFormat.DayNames.Contains(day, StringComparer.OrdinalIgnoreCase))
                throw new BusinessException("Geçersiz Gün İsmi!");

            DateTime startsOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday);

            int dayIndex = Array.IndexOf(trCulture.DateTimeFormat.DayNames, day);

            dates.Add(startsOfWeek.AddDays(dayIndex));
        }

        return dates;
    }
}

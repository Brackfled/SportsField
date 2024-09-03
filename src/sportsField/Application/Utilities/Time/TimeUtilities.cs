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
        CultureInfo enCulture = new CultureInfo("en-US");
        string[] englishDayNames = enCulture.DateTimeFormat.DayNames;

        int firstDayOfWeek = (int)DayOfWeek.Monday;

        string[] reorderedDayNames = englishDayNames.Skip(1).Concat(englishDayNames.Take(1)).ToArray();

        IList<DateTime> dates = new List<DateTime>();
        foreach (string day in retentionCommandDto.ReservationDays)
        {
            if (!reorderedDayNames.Contains(day, StringComparer.OrdinalIgnoreCase))
                throw new BusinessException("Geçersiz Gün İsmi!");

            DateTime startsOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + firstDayOfWeek);

            int dayIndex = Array.IndexOf(reorderedDayNames, day);

            dates.Add(startsOfWeek.AddDays(dayIndex));
        }

        return dates;
    }
}

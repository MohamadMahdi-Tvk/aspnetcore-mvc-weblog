using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WeblogSample.Service.Extentions;

public static class DateExtensions
{
    public static string ToShamsi(this DateTime date)
    {
        PersianCalendar pc = new PersianCalendar();

        int year = pc.GetYear(date);
        int month = pc.GetMonth(date);
        int day = pc.GetDayOfMonth(date);

        return $"{year:0000}/{month:00}/{day:00}";
    }
}

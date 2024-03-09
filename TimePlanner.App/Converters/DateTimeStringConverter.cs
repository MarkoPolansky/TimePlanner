﻿using CommunityToolkit.Maui.Converters;
using System.Globalization;

namespace TimePlanner.App.Converters;

public class DateTimeStringConverter : BaseConverterOneWay<DateTime?, string>
{
    public override string DefaultConvertReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override string ConvertFrom(DateTime? value, CultureInfo? culture)
        => value == null ? "" : ((DateTime)value).ToString("dd. MM. yyyy HH:mm");

}
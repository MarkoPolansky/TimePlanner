using CommunityToolkit.Maui.Converters;
using System.Globalization;
using TimePlanner.BL.Models;

namespace TimePlanner.App.Converters;

public class UserNameConverter : BaseConverterOneWay<UserListModel, string>
{
    public override string DefaultConvertReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override string ConvertFrom(UserListModel value, CultureInfo? culture)
        => value.FirstName + " " + value.LastName;
}
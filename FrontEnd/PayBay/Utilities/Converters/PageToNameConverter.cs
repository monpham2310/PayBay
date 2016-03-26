using System;
using Windows.UI.Xaml.Data;
using PayBay.View.BottomFunctionGroup.SettingGroup;
using PayBay.View.TopFunctionGroup;
using PayBay.View.MiddleFunctionGroup;
using PayBay.View.StartGroup;

namespace PayBay.Utilities.Converters
{
    /// <summary>
    /// Convert the page type to the name that display in the title
    /// </summary>
    public class PageToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string defaultName = "PayBay";

            if (value is StartPage)
            {
                return defaultName;
            }

			if (value is HomePage)
			{
				return "HomePage";
			}

            if (value is AddMarketPage)
            {
                return "Function 1";
            }

            if (value is AddProductPage)
            {
                return "Function 2";
            }

            if (value is SettingPage)
            {
                return "Settings";
            }

            return defaultName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}

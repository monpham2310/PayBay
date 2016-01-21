using System;
using Windows.UI.Xaml.Data;
using SplitViewSampleWithMVVM.View.BottomFunctionGroup.SettingGroup;
using SplitViewSampleWithMVVM.View.FunctionGroup;
using SplitViewSampleWithMVVM.View.StartGroup;

namespace SplitViewSampleWithMVVM.Utilities.Converters
{
    /// <summary>
    /// Convert the page type to the name that display in the title
    /// </summary>
    public class PageToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string defaultName = "Splitview Sample with MVVM";

            if (value is StartPage)
            {
                return defaultName;
            }

            if (value is Function1Page)
            {
                return "Function 1";
            }

            if (value is Function2Page)
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

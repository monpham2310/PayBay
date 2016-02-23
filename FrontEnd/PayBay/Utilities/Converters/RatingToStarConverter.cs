using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace PayBay.Utilities.Converters
{
    public class RatingToStarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                string emptyStar = "ms-appx:///Assets/Rating/emptystar.png";
                string fullStar = "ms-appx:///Assets/Rating/fullstar.png";
                string halfStar = "ms-appx:///Assets/Rating/halfstar.png";

                string starImageResult = emptyStar;
                float cal = (float)value - Int32.Parse((string)parameter);

                if (cal >= 0)
                    starImageResult = fullStar;
                else if (cal >= -0.5)
                    starImageResult = halfStar;

                return starImageResult;

            }
            catch (Exception ex)
            {                
                System.Diagnostics.Debug.WriteLine("EXCEPTIONNN: " + ex.ToString());
                return "Assets/lol.jpg";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                return 5.0f;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

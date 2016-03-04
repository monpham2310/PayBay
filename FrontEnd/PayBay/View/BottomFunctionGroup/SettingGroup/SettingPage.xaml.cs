using System.Diagnostics;
using Windows.UI.Xaml.Controls;

namespace PayBay.View.BottomFunctionGroup.SettingGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        //private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    PivotItem p = SettingPivot.SelectedItem as PivotItem;
        //    Debug.Assert(p != null, "p != null");
        //    switch (p.Tag.ToString())
        //    {
        //        case "about":
        //        {
        //            SettingFrame.Navigate(typeof (AboutPage));
        //            break;
        //        }
        //        case "rate":
        //        {
        //            SettingFrame.Navigate(typeof (RateAndFeedbackPage));
        //            break;
        //        }
        //    }
        //}
    }
}
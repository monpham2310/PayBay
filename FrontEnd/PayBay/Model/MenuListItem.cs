using PayBay.ViewModel;

namespace PayBay.Model
{
    public enum MenuFunc
    {
		HomePage,
		Inbox,
		MyPayBay,
		MyFavorites,
        Func1,
        Func2,
        Func3,
		FreeCall,
		AboutUs,
		Logout,
		FeedbackAndApps,
        Settings
    };

    public class MenuListItem : BaseViewModel
    {

        private string _name;
        private string _icon;
        private MenuFunc _menuF;
        private bool _isEnabled;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Icon
        {
            get { return _icon; }
            set
            {
                if (value == _icon) return;
                _icon = value;
                OnPropertyChanged();
            }
        }

        public MenuFunc MenuF
        {
            get { return _menuF; }
            set
            {
                if (value == _menuF) return;
                _menuF = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value.Equals(_isEnabled)) return;
                _isEnabled = value;
                OnPropertyChanged();
            }
        }
    }
}

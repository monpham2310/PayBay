using System.ComponentModel;
using System.Runtime.CompilerServices;
using PayBay.Annotations;

namespace PayBay.ViewModel
{
    /// <summary>
    /// Implement PropertyChanged event handler and method for every class that inherit from this class
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator] 
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

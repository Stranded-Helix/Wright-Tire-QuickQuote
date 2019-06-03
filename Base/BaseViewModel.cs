using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WrightTireEstimator
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = " ")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

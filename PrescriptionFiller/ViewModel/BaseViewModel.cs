using System;
using System.ComponentModel;

namespace PrescriptionFiller.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string proprtyname)
        {
            try
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(proprtyname));
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}

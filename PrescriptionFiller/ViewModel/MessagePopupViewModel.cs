using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrescriptionFiller.ViewModel
{
    public class MessagePopupViewModel : BaseViewModel
    {
        private INavigation navigation;

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        private ImageSource _icon;
        public ImageSource Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                OnPropertyChanged("Icon");
            }
        }

        private ImageSource _buttonImage;
        public ImageSource ButtonImage
        {
            get { return _buttonImage; }
            set
            {
                _buttonImage = value;
                OnPropertyChanged("ButtonImage");
            }
        }

        private Color _popupColor;
        public Color PopupColor
        {
            get { return _popupColor; }
            set
            {
                _popupColor = value;
                OnPropertyChanged("PopupColor");
            }
        }

        public ICommand CloseCommand { get; set; }


        public MessagePopupViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            CloseCommand = new Command(() =>
            {
                this.navigation.PopModalAsync();
            });
        }
    }
}

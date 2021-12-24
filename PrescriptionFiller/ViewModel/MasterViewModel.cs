using System;
using System.Collections.ObjectModel;
using PrescriptionFiller.Views;

namespace PrescriptionFiller.ViewModel
{
    public class MasterViewModel : BaseViewModel
    {
        public ObservableCollection<HomeViewMenuItem> MenuItems { get; set; }

        public MasterViewModel()
        {
            MenuItems = new ObservableCollection<HomeViewMenuItem>(new[]
            {
                    new HomeViewMenuItem { Id = 0, Title = "Prescription", image = "pfa_icon_edit_small" ,TargetType= typeof(HomePage)},
                    new HomeViewMenuItem { Id = 1, Title = "Account Info", image = "pfa_icon_user_small" ,TargetType= typeof(AccountInfoView)},
                    new HomeViewMenuItem { Id = 2, Title = "Medical History" , image = "pfa_icon_home_small",TargetType= typeof(MedicalHistoryView)},
                    new HomeViewMenuItem { Id = 3, Title = "Logout", image = "pfa_icon_lock_small" ,TargetType= typeof(LoginView)},
            });
        }
    }
}

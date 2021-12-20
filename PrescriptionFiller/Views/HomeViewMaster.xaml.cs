using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PrescriptionFiller.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrescriptionFiller.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeViewMaster : ContentPage
    {
        public ListView ListView;

        public HomeViewMaster()
        {
            InitializeComponent();

            BindingContext = new MasterViewModel();
            ListView = MenuItemsListView;
        }
    }
}

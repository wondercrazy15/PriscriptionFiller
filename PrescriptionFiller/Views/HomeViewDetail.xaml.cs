using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrescriptionFiller.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace PrescriptionFiller.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeViewDetail : Xamarin.Forms.TabbedPage
    {
        public HomeViewDetail()
        {
            InitializeComponent();
            //BindingContext = new HomeViewModel(Navigation);
            //On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}

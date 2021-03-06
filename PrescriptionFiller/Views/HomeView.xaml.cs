using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrescriptionFiller.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : MasterDetailPage
    {
        public HomeView()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as HomeViewMenuItem;
            if (item == null)
                return;

            if (item.Id == 3)
            {
                Application.Current.MainPage = new LoginView();
            }
            else
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;
                Detail = new NavigationPage(page);
                IsPresented = false;
                MasterPage.ListView.SelectedItem = null;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}

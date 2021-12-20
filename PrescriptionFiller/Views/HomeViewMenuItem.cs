using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrescriptionFiller.Views
{
    public class HomeViewMenuItem
    {
        //public HomeViewMenuItem()
        //{
        //    TargetType = typeof(HomeViewDetail);
        //}
        public int Id { get; set; }
        public string Title { get; set; }
        public ImageSource image { get; set; }
        public Type TargetType { get; set; }

    }
}

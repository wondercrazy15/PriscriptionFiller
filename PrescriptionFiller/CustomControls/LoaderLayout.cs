using System;
using Xamarin.Forms;

namespace PrescriptionFiller.CustomControls
{
    public delegate void LayoutChildrenDelegate(double x, double y, double width, double height);

    public class LoaderLayout : AbsoluteLayout
    {
        public event LayoutChildrenDelegate OnLayoutChildren;

        public bool IsHandlingLayoutManually { get; set; }

        public LoaderLayout()
        {
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            if (!IsHandlingLayoutManually)
            {
                base.LayoutChildren(x, y, width, height);
            }

            if (OnLayoutChildren != null)
            {
                OnLayoutChildren(x, y, width, height);
            }

            Console.WriteLine(string.Format("SimpleLayout.LayoutChildren({0}, {1}, {2}, {3});", x, y, width, height));
        }

        public void InvalidateSimpleLayout()
        {
            InvalidateLayout();
        }
    }
}

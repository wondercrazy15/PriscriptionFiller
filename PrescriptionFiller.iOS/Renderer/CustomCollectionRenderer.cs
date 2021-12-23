using System;
using CoreGraphics;
using PrescriptionFiller.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly : ExportRenderer(typeof(CollectionView), typeof(CustomCollectionRenderer))]
namespace PrescriptionFiller.iOS.Renderer
{
    public class CustomCollectionRenderer : CollectionViewRenderer
    {
        public CustomCollectionRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<GroupableItemsView> e)
        {
            base.OnElementChanged(e);
            this.Layer.ShadowRadius = 20;
            Layer.ShadowOpacity = 0;
        }
    }
}

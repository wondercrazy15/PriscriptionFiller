using System;
using PrescriptionFiller.Droid.Renderer;
using PrescriptionFiller.interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobRenderer))]
namespace PrescriptionFiller.Droid.Renderer
{
    public class AdMobRenderer : ViewRenderer<AdMobView, Android.Gms.Ads.AdView>
    {
        [Obsolete]
        public AdMobRenderer()
        {

        }
              protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var ad = new Android.Gms.Ads.AdView(Forms.Context);
                //ad.AdSize = Android.Gms.Ads.AdSize.Banner;
                ad.AdSize = Android.Gms.Ads.AdSize.SmartBanner;
                ad.AdUnitId = "ca-app-pub-1197085349048387/7528195950";

                var requestbuilder = new Android.Gms.Ads.AdRequest.Builder();
                ad.LoadAd(requestbuilder.Build());

                SetNativeControl(ad);
            }
        }
    }
}


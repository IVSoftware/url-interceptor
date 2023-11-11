using Foundation;
using UIKit;

namespace url_interceptor
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
        public override bool OpenUrl(UIApplication application, NSUrl url, NSDictionary options)
        {
            if(url.AbsoluteString.Contains("net.ivsoftware.demo"))
            {
                Task
                    .Delay(TimeSpan.FromSeconds(0.5))
                    .GetAwaiter()
                    .OnCompleted(() =>
                    {
                        App.Current.MainPage.DisplayAlert("Interceptor", "Hello", "OK");
                    });
                return true;
            }
            else
            {
                return base.OpenUrl(application, url, options);
            }
        }
    }
}
# Url Interceptor

What has worked for me is to implement this in platform-specific code. For example, this works for Android.

```
using Android.App;
using Android.Content;
using Android.OS;

namespace url_interceptor.Platforms.Android
{
    [Activity(Label = "UrlInterceptorActivity", Exported =true)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataSchemes = new[] { "net.ivsoftware.demo" })]
    public class UrlInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            _ = App.Current.MainPage.DisplayAlert("Interceptor", "Hello", "OK");
            base.OnCreate(savedInstanceState);

            Intent intent = Intent;
            var uri = intent.Data;

            Finish(); // Close the activity
        }
    }
}
```
You can intercept more recognizable data schemes such as `http://` and `https://` but in this case I'm using **`net.ivsoftware.demo://`** which is custom for this app.

It's likely to be mistaken for a search if you just "type it into the search bar" so testing it usually requires a button. I made one at http://www.ivsoftware.net/interceptor-test.html that will fire this URL in order to test this app. Feel free to clone my [repo](https://github.com/IVSoftware/url-interceptor.git) and try it on Android or iOS (you'll need to provision it to build on iOS of course). It's working "here" and should work for you, or at least give you a really good start in getting it working where you are.

___

For iOS the Info.plist will need to be edited in the `Advanced` tab:

[![Info.plist][1]][1]

And handled in AppDelegate.cs.

```
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
```
More info on [Apple Developer](https://developer.apple.com/documentation/xcode/defining-a-custom-url-scheme-for-your-app)

  [1]: https://i.stack.imgur.com/l06bS.png

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
You can intercept more recognizable data schemes such as `http://` and `https://` but in this case the one I'm using is custom for this demo app - it is set to respond to will respond to `net.ivsoftware.demo://`.

It's likely to be mistaken for a search if you just "type it into the search bar" so testing it usually requires a button. I made one at http://www.ivsoftware.net/interceptor-test.html that will fire this URL in order to test this app.
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


  [1]: https://i.stack.imgur.com/l06bS.png

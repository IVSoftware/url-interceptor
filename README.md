# Url Interceptor

What has worked for me is to implement this as a platform-specific function. Here's an example for Android.

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

This will respond to `net.ivsoftware.demo://` but testing it usually requires more than just typing it in the search bar. Feel free to test-fire this in your browser using http://www.ivsoftware.net/interceptor-test.html.

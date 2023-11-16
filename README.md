# Url Interceptor

Here's what works for me in Android platform. I can launch my app one of two ways:

___

##### Navigate to https://www.ivsoftware.net/interceptor-test.html and click the button:


[![deep-linking][1]][1]

___

#### From this or any other web page, click the Google Chrome Share Icon instead:

[![share icon][2]][2]

___

Here's the [Android Intent Filter](https://developer.android.com/guide/components/intents-filters) code.
```
using Android.App;
using Android.Content;
using Android.OS;
using url_interceptor;

namespace url_interceptor.Platforms.Android
{
    [Activity(Label = "UrlInterceptorActivity", Exported =true)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[]
        { 
            Intent.CategoryDefault, 
            Intent.CategoryBrowsable 
        },
        DataSchemes = new[] 
        {
            "net.ivsoftware.demo" 
        })]

    [IntentFilter(
        new[]
        { Intent.ActionSend },
        Categories = new[]
        {
            Intent.CategoryDefault,
        },
        DataMimeType = "text/plain"
    )]
    public class UrlInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Intent intent = Intent;
            var uri = intent.Data;
            StartMainActivity();
            Task
                .Delay(TimeSpan.FromSeconds(0.5))
                .GetAwaiter()
                .OnCompleted(() =>
                {
                    switch (this.Intent.Action)
                    {
                        case Intent.ActionView:
                            if (intent.Action == Intent.ActionView)
                            {
                                var uri = intent.Data;
                                App.Current.MainPage.DisplayAlert("Interceptor", "Deep Link Button", "OK");
                            }
                            break;
                        case Intent.ActionSend:
                            var link = intent.GetStringExtra(Intent.ExtraText);
                            App.Current.MainPage.DisplayAlert("Interceptor", $"Shared Link: '{link}'", "OK");
                            break;
                    }
                });
            Finish(); 
        }

        private void StartMainActivity()
        {
            Intent mainActivityIntent = new Intent(this, typeof(MainActivity));
            StartActivity(mainActivityIntent);
        }
    }
}
```
___

iOS has a similar mechanism for intercepting URLs but I haven't tested this particular action.


  [1]: https://i.stack.imgur.com/gy5rw.png
  [2]: https://i.stack.imgur.com/MHhnN.png
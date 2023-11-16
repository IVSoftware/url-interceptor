using Android.App;
using Android.Content;
using Android.OS;
using url_interceptor;

namespace url_interceptor.Platforms.Android
{
    [Activity(Label = "UrlInterceptorActivity", Exported = true)]
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
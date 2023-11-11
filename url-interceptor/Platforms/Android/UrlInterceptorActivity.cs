using Android.App;
using Android.Content;
using Android.OS;
using url_interceptor;

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
            base.OnCreate(savedInstanceState);

            Intent intent = Intent;
            var uri = intent.Data;
            StartMainActivity();
            Task
                .Delay(TimeSpan.FromSeconds(0.5))
                .GetAwaiter()
                .OnCompleted(() =>
                {
                    App.Current.MainPage.DisplayAlert("Interceptor", "Hello", "OK");
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


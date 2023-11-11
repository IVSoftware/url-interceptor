using Android.App;
using Android.Content;
using Android.OS;

namespace url_interceptor.Platforms.Android
{
    [Activity(
        Label = "UrlInterceptorActivity",
        MainLauncher = true)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.ActionView, Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataSchemes = new[] { "http", "https" },
        DataHost = "www.ivsoftware.net", 
        DataPathPrefix = "/demo",
        AutoVerify = true)]
    public class UrlInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Intent intent = Intent;
            var uri = intent.Data;

            _ = App.Current.MainPage.DisplayAlert("Interceptor", "Hello", "OK");

            Finish(); 
        }
    }
}


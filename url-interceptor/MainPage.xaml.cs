using System.Diagnostics;

namespace url_interceptor
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Delay(1000).GetAwaiter().OnCompleted(() => 
            {
                var mainBundlePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Debug.Write(string.Join(Environment.NewLine, Directory.GetDirectories(mainBundlePath)));
                { }
            });
        }
    }
}
namespace ProductivityApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnNewGoalButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NewGoalPage));
        }

        private async void OnGoalTapped(object sender, TappedEventArgs e)
        {
            if(sender is Border border && border.BindingContext is Goal goal)
            {
                await Shell.Current.GoToAsync($"{nameof(DetailPage)}", true,
                    new Dictionary<string, object>
                    {
                        { "Goal", goal }
                    });
            }
        }
    }
}

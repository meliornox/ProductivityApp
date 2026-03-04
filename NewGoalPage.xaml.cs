namespace ProductivityApp;

public partial class NewGoalPage : ContentPage
{
	public NewGoalPage()
	{
		InitializeComponent();
	}

    private async void OnSubmitButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
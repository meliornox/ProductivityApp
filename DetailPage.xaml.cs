namespace ProductivityApp;

[QueryProperty(nameof(Goal), "Goal")]

public partial class DetailPage : ContentPage
{
	private Goal _goal;

	public Goal Goal
	{
		get => _goal;
		set
		{
			_goal = value;

			OnPropertyChanged();
		}
	}

    public DetailPage()
	{
		InitializeComponent();

		BindingContext = this;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
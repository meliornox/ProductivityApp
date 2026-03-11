using ProductivityApp.ViewModels;

namespace ProductivityApp.Views;

public partial class NewGoalPage : ContentPage
{
	public NewGoalPage(NewGoalViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;

    }
}
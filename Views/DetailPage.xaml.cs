using ProductivityApp.ViewModels;

namespace ProductivityApp.Views;

public partial class DetailPage : ContentPage
{

    public DetailPage(DetailViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;
    }
}
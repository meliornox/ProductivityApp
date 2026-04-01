using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.Views;

namespace ProductivityApp.ViewModels;

// first nameof(Goal) is the class's property
// second "Goal" defined in the path
// await Shell.Current.GoToAsync($"{nameof(DetailPage)}"); Goal={object}
[QueryProperty(nameof(Goal), "Goal")]

public partial class DetailViewModel : ObservableObject
{
    private readonly IGoalService _goalService;

    [ObservableProperty]
    Goal goal;

    // Separate Observable Property for every input field
    [ObservableProperty]
    private string id;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string motivation;

    [ObservableProperty]
    private DateTime targetDate;

    public DetailViewModel(IGoalService goalService)
    {
        _goalService = goalService;
    }

    public void OnAppearing()
    {
        if (Goal is not null)
        {
            Name = Goal.Name;
            Motivation = Goal.Motivation;
            TargetDate = Goal.TargetDate;
        }
    }

    //Edit function
    [RelayCommand]
    async Task EditButtonClicked()
    {
        await Shell.Current.GoToAsync($"{nameof(NewGoalPage)}");
    }

    //Delete function
    [RelayCommand]
    async Task DeleteButtonClicked()
    {
        if (goal is not null)
        {
            await _goalService.DeleteItemAsync(goal);
        }
        else
        {
            await Shell.Current.DisplayAlertAsync("Error!", "No goal to delete", "OK");
        }

        await Shell.Current.GoToAsync("..");
    }

    //Back button
    [RelayCommand]
    async Task BackButtonClicked()
    {
        await Shell.Current.GoToAsync("..");
    }
}

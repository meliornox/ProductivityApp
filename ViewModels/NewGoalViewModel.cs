using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.Views;

namespace ProductivityApp.ViewModels;

[QueryProperty(nameof(Goal), "Goal")]

public partial class NewGoalViewModel : ObservableObject
{
    private readonly IGoalService _goalService;

    [ObservableProperty]
    Goal goal;

    [ObservableProperty]
    private string title;

    // Separate Observable Property for every input field
    [ObservableProperty]
    private string id;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string motivation;

    [ObservableProperty]
    private DateTime targetDate = DateTime.Today; // default value

    public NewGoalViewModel(IGoalService goalService)
    {
        _goalService = goalService;
    }

    public void OnAppearing()
    {
        Title = Goal is null ? "New Goal" : $"Edit: {Goal.Name}";

        if (Goal is not null)
        {
            Name = Goal.Name;
            Motivation = Goal.Motivation;
            TargetDate = Goal.TargetDate;
        }
    }

    /*

    //Example command implemented with Noemi
    [RelayCommand]

    async Task AddNewGoal()
    {
        Goal goal = new Goal()
        {
            Name = Name,
            Motivation = Motivation,
            TargetDate = TargetDate
        };

        await _goalService.SaveItemAsync(goal);

        await Shell.Current.GoToAsync("..");
    }
    
    */

    [RelayCommand]

    async Task AddNewGoal()
    {
        //Goal goal = new Goal();
        Goal goal;
        if (Goal is null)
        {
            goal = new Goal()
            {
                Name = Name,
                Motivation = Motivation,
                TargetDate = TargetDate
            };
        }
        else
        {
            goal = Goal;
            goal.Id = Goal.Id;
            goal.Name = Name;
            goal.Motivation = Motivation;
            goal.TargetDate = TargetDate;
        }

        //If actual app, would use try-catch to see if it's successful
        await _goalService.SaveItemAsync(goal);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]

    async Task BackButtonClicked()
    {
        await Shell.Current.GoToAsync("..");
    }
}

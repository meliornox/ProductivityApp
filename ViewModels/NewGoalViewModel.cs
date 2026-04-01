using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.Views;

namespace ProductivityApp.ViewModels;

[QueryProperty(nameof(Goal), "Goal")]

public partial class NewGoalViewModel : ObservableObject
{
    // Establish private database access
    private readonly IGoalService _goalService;

    // Make the new goal observable
    [ObservableProperty]
    Goal goal;

    // For use to distinguish the Edit page from the New Goal page
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

    /// <summary>
    /// When the page is opened
    /// refresh the title based on making or editing a goal. 
    /// If editing a goal, populate fillable fields with existing data.
    /// </summary>
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
    
    /// <summary>
    /// When adding a new goal,
    /// if the goal doesn't already exist, create one
    /// but if it does exist, set the current values to the new values
    /// 
    /// TODO: Try-catch blocks
    /// </summary>
    /// <returns></returns>
    [RelayCommand]

    async Task AddNewGoal()
    {
        Goal goal;
        if (Goal is null)
        {
            goal = new Goal()
            {
                Name = Name,
                Motivation = Motivation,
                TargetDate = TargetDate
            };

            //If actual app, would use try-catch to see if it's successful
            await _goalService.SaveItemAsync(goal);
        }
        else
        {
            goal = Goal;
            goal.Id = Goal.Id;
            goal.Name = Name;
            goal.Motivation = Motivation;
            goal.TargetDate = TargetDate;

            //If actual app, would use try-catch to see if it's successful
            await _goalService.SaveItemAsync(goal);

            await Shell.Current.GoToAsync("..");
        }
        await Shell.Current.GoToAsync("..");
    }

    /// <summary>
    /// On user input of tapping or clicking the back button
    /// goes back to Main View or Detail View
    /// </summary>
    /// <returns>Error if not able to go back</returns>
    [RelayCommand]

    async Task BackButtonClicked()
    {
        await Shell.Current.GoToAsync("..");
    }
}

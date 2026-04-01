using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.Views;
using ProductivityApp.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace ProductivityApp.ViewModels;

// first nameof(Goal) is the class's property
// second "Goal" defined in the path
[QueryProperty(nameof(Goal), "Goal")]

public partial class DetailViewModel : ObservableObject
{
    // Private access to the GoalService
    private readonly IGoalService _goalService;

    // Read the current Goal
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

    /// <summary>
    /// Sets up private access to the GoalService
    /// </summary>
    /// <param name="goalService">GoalService to connect</param>
    public DetailViewModel(IGoalService goalService)
    {
        _goalService = goalService;
    }

    /// <summary>
    /// When the DetailPage is pulled up, refresh the goal. In this case pull over the properties passed by the database about the goals into the DetailView.
    /// </summary>
    public void OnAppearing()
    {
        if (Goal is not null)
        {
            Name = Goal.Name;
            Motivation = Goal.Motivation;
            TargetDate = Goal.TargetDate;
        }
    }

    /// <summary>
    /// On user input of tapping or clicking an item
    /// goes to edit page
    /// passing goal object to edit page for modification
    /// </summary>
    [RelayCommand]

    async Task EditButtonClicked()
    {
        if (goal is null)
        {
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(NewGoalPage)}", true,
            new Dictionary<string, object>
            {
                { "Goal", goal },
            });
    }

    /// <summary>
    /// On user input of tapping or clicking the delete button
    /// deletes goal
    /// </summary>
    /// <returns>Error if nothing passed to delete</returns>
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

    /// <summary>
    /// On user input of tapping or clicking the back button
    /// goes back to Main View
    /// </summary>
    /// <returns>Error if not able to go back</returns>
    [RelayCommand]
    async Task BackButtonClicked()
    {
        await Shell.Current.GoToAsync("..");
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductivityApp.DAL;
using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace ProductivityApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IGoalService _goalService;

    public ObservableCollection<Goal> Goals { get; } = new();

    public MainViewModel(IGoalService goalService)
    {
        _goalService = goalService;
    }

    /// <summary>
    /// Refreshes the goals on the UI when the main view appears on screen.
    /// </summary>
    public void OnAppearing()
    {
        GetGoalsAsync();
    }

    /// <summary>
    /// Refresh the Goals collection.
    /// For use when changing the list of goals to update the UI.
    /// </summary>
    async void GetGoalsAsync()
    {
        try
        {
            var goals = await _goalService.GetItemsAsync();

            if (Goals.Count != 0)
            {
                Goals.Clear();
            }
            foreach (var goal in goals)
            {
                Goals.Add(goal);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlertAsync("Error!", "Unable to get goals", "OK");
        }
    }

    /// <summary>
    /// On user input of tapping or clicking the "Add Goal" button
    /// Goes to New Goal page
    /// </summary>
    [RelayCommand]

    async Task GoToAddAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(NewGoalPage)}");
    }

    /// <summary>
    /// On user input of tapping or clicking an item
    /// goes to detail page
    /// passing goal object to details page for display
    /// </summary>
    [RelayCommand]

    async Task GoToDetailsAsync(Goal goal)
    {
        if (goal is null)
        {
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(DetailPage)}", true,
            new Dictionary<string, object>
            {
                { "Goal", goal },
            });
    }

    /// <summary>
    /// Function to add Goal object to Goals database
    /// </summary>
    [RelayCommand]

    async Task AddNewGoal(Goal goal)
    { 
        await _goalService.SaveItemAsync(goal);
    }

    /// <summary>
    /// On user input of tapping or clicking an item
    /// Goes to detail page
    /// Passing goal object to details page for display
    /// </summary>
    [RelayCommand]

    async Task Delete(Goal goal)
    {
        if (goal is null)
        {
            return;
        }

        //professional if try/catch with popup error
        await _goalService.DeleteItemAsync(goal);

        GetGoalsAsync();
    }
}
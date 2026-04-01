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

    /// <summary>
    /// On user input of tapping or clicking an item
    /// goes to detail page
    /// passing goal object to details page for display
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

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductivityApp.Models;
using ProductivityApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProductivityApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public ObservableCollection<Goal> Goals { get; }

    public MainViewModel()
    {
        Goals = new ObservableCollection<Goal> {
            new Goal()
            {
                Name = "First Goal",
                Motivation = "The first goal is important.",
                TargetDate = "February 18, 2026",
                Progress = 0.8,
            },
            new Goal()
            {
                Name = "Second Goal",
                Motivation = "The second goal is important.",
                TargetDate = "February 18, 2026",
                Progress = 0.7
            },
            new Goal()
            {
                Name = "Third Goal",
                Motivation = "The third goal is important.",
                TargetDate = "February 18, 2026",
                Progress = 0.6
            },
            new Goal()
            {
                Name = "Fourth Goal",
                Motivation = "The fourth goal is important.",
                TargetDate = "February 18, 2026",
                Progress = 0.5
            },
            new Goal()
            {
                Name = "Fifth Goal",
                Motivation = "The fifth goal is important.",
                TargetDate = "February 18, 2026",
                Progress = 0.4
            },
            new Goal()
            {
                Name = "Sixth Goal",
                Motivation = "The sixth goal is important.",
                TargetDate = "February 18, 2026",
                Progress = 0.3
            }
        };

    }

    // Navigate to Add page to user input
    [RelayCommand]
    async Task GoToAddAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(NewGoalPage)}");
    }

    public void AddNewGoal(Goal goal)
    {
        Goals.Add(goal);
    }

    // Need user input -> user push an item
    // Navigate to details page
    // Give goal object to details page
    [RelayCommand]
    async Task GoToDetailsAsync(Goal goal)
    {
        if (goal is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(DetailPage)}", true,
            new Dictionary<string, object>
            {
            { "Goal", goal },
            });
    }
}
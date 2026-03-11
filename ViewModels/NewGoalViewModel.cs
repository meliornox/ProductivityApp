using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductivityApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityApp.ViewModels;

public partial class NewGoalViewModel : ObservableObject
{
    // Separate Observable Property for every input field
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string motivation;

    [ObservableProperty]
    private DateTime targetDate = DateTime.Today; // default value

    [ObservableProperty]
    private double progress;

    /*
    public NewGoalViewModel()
    {

    } 
    */

    [RelayCommand]
    async Task AddNewGoal()
    {
        Goal goal = new Goal()
        {
            Name = Name,
            Motivation = Motivation,
            TargetDate = TargetDate.ToString("MMMM dd, yyyy"),
            Progress = Progress
        };

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task BackButtonClicked()
    {
        await Shell.Current.GoToAsync("..");
    }
}

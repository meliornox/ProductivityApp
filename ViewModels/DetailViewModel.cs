using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductivityApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityApp.ViewModels;

// first nameof(Goal) is the class's property
// second "Goal" defined in the path
// await Shell.Current.GoToAsync($"{nameof(DetailPage)}"); Goal={object}
[QueryProperty(nameof(Goal), "Goal")]

public partial class DetailViewModel : ObservableObject
{
    [ObservableProperty]
    Goal goal;

    [RelayCommand]
    async Task BackButtonClicked()
    {
        await Shell.Current.GoToAsync("..");
    }
}

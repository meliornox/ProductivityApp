using System;
using ProductivityApp.Models;

namespace ProductivityApp.Services;

public interface IGoalService
{
	Task<List<Goal>> GetItemsAsync();
	Task<Goal> GetItemAsync(int id);
	Task<int> SaveItemAsync(Goal goal);
    Task<int> DeleteItemAsync(Goal goal);
}

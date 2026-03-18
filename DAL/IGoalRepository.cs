using System;

namespace ProductivityApp.DAL;

public interface IGoalRepository
{
    Task<List<GoalEntity>> GetItemsAsync();
    Task<GoalEntity> GetItemAsync(int id);
    Task<int> SaveItemAsync(GoalEntity goal);
    Task<int> DeleteItemAsync(GoalEntity goal);
}


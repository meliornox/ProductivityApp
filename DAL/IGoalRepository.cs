namespace ProductivityApp.DAL;

/// <summary>
/// GoalRepository interface
/// 
/// TODO Polish: Delete by ID instead of by Goal. This would save memory by only copying the integer for the ID and not copying the rest of the goal when calling the method.
/// </summary>
public interface IGoalRepository
{
    Task<List<GoalEntity>> GetItemsAsync();
    Task<GoalEntity> GetItemAsync(int id);
    Task<int> SaveItemAsync(GoalEntity goal);
    Task<int> DeleteItemAsync(GoalEntity goal);
}


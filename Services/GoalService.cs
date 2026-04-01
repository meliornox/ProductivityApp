using ProductivityApp.DAL;
using ProductivityApp.Models;

namespace ProductivityApp.Services;

/// <summary>
/// Provides access layer for frontend to the GoalRepository database access layer. Translates between the GoalEntity that is stored in the database and the Goal used for the user interface.
/// </summary>
class GoalService : IGoalService
{

	private readonly IGoalRepository _goalRepository;

	/// <summary>
	/// Provide private access to the GoalRepository
	/// </summary>
	/// <param name="goalRepository"></param>
	public GoalService(IGoalRepository goalRepository)
	{
		_goalRepository = goalRepository;
    }	
	
	/// <summary>
	/// Get Goal by ID from GoalRepository
	/// </summary>
	/// <param name="id">GoalEntity ID</param>
	/// <returns>Goal of Entity</returns>
    public async Task<Goal> GetItemAsync(int id)
    {
        var entity = await _goalRepository.GetItemAsync(id);

        return ToGoal(entity);
    }

	/// <summary>
	/// Retrieves a list of all goals 
	/// </summary>
	/// <returns>list of Goals from GoalEntities</returns>
    public async Task<List<Goal>> GetItemsAsync()
	{
		var list = await _goalRepository.GetItemsAsync();

		return list.Select(entity => ToGoal(entity)).OrderBy(entity => entity.TargetDate).ToList();
    }

    //Example command implemented with Noemi
	/// <summary>
	/// Save a Goal to the database, updating an existing record or making a new one if it doesn't exist yet
	/// </summary>
	/// <param name="goal">Goal being saved</param>
	/// <returns>Error if not able to to save</returns>
    public async Task<int> SaveItemAsync(Goal goal)
	{
		GoalEntity entity;
		if (goal.Id is null)
		{
			entity = new()
			{
				Name = goal.Name,
				Motivation = goal.Motivation,
				StartDate = DateTime.Now,
				TargetDate = goal.TargetDate
			};
		}
		else
		{
			entity = ToEntity(goal);
		}

		return await _goalRepository.SaveItemAsync(entity);
	}

	/// <summary>
	/// Deletes a Goal from the database
	/// </summary>
	/// <param name="goal">Goal to be deleted</param>
	/// <returns>Error if unable to delete</returns>
    public async Task<int> DeleteItemAsync(Goal goal)
	{
		GoalEntity entity = ToEntity(goal);

		return await _goalRepository.DeleteItemAsync(entity);
	}

	/// <summary>
	/// Converts frontend Goal to backend GoalEntity
	/// </summary>
	/// <param name="goal">Goal to convert</param>
	/// <returns>Entity of Goal</returns>
	GoalEntity ToEntity(Goal goal)
	{
		return new GoalEntity()
		{
			Id = (int)goal.Id,
			Name = goal.Name,
			Motivation = goal.Motivation,
			StartDate = goal.StartDate,
			TargetDate = goal.TargetDate
		};
	}

	/// <summary>
	/// Converts backend GoalEntity to frontend Goal
	/// </summary>
	/// <param name="entity">GoalEntity to convert</param>
	/// <returns>Goal of entity</returns>
	Goal ToGoal(GoalEntity entity)
	{
		return new Goal
		{
			Id = entity.Id,
			Name = entity.Name,
			Motivation = entity.Motivation,
			StartDate = entity.StartDate,
			TargetDate = entity.TargetDate,
			Progress = getProgress(entity.StartDate, entity.TargetDate)
		};
	}

    // Business logic

    /// <summary>
    /// Make Progress as a percentage of days from StartDate until TargetDate
    /// </summary>
    /// <param name="start">DateTime when the goal was created</param>
    /// <param name="target">Datetime when the goal will be ending or "due"</param>
    /// <returns>Ratio of days passed to total days in order to display the data as a progressbar</returns>
    private double getProgress(DateTime start, DateTime target)
	{
		TimeSpan allDifference = target - start;
		double allTotalDays = allDifference.TotalDays;

		TimeSpan currentDifference = start - DateTime.Now;
		double currentTotalDays = currentDifference.TotalDays;

		double progress = currentTotalDays / allTotalDays;

		return progress;
	}
}

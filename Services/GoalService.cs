using ProductivityApp.DAL;
using ProductivityApp.Models;

namespace ProductivityApp.Services;

class GoalService : IGoalService
{
	private readonly IGoalRepository _goalRepository;

	public GoalService(IGoalRepository goalRepository)
	{
		_goalRepository = goalRepository;
    }	
	
    public async Task<Goal> GetItemAsync(int id)
    {
        var entity = await _goalRepository.GetItemAsync(id);

        return ToGoal(entity);
    }

    public async Task<List<Goal>> GetItemsAsync()
	{
		var list = await _goalRepository.GetItemsAsync();

		return list.Select(entity => ToGoal(entity)).OrderBy(entity => entity.TargetDate).ToList();
    }

    //Example command implemented with Noemi
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

    public async Task<int> DeleteItemAsync(Goal goal)
	{
		GoalEntity entity = ToEntity(goal);

		return await _goalRepository.DeleteItemAsync(entity);
	}

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

    //Business logic

    //Make Progress as a percentage of days from StartDate until TargetDate
	private double getProgress(DateTime start, DateTime target)
	{
		TimeSpan allDifference = target - start;
		double allTotalDays = allDifference.TotalDays;

		TimeSpan currentDifference = DateTime.Now - start;
		double currentTotalDays = currentDifference.TotalDays;

		double progress = currentTotalDays / allTotalDays;

		if (progress < 0)
		{
			progress = 1;
		}

		return progress;
	}
}

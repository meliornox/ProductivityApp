using System;
using ProductivityApp.DAL;
using ProductivityApp.Models;

namespace ProductivityApp.Services;

class GoalService : IGoalService
{
	private readonly GoalRepository _goalRepository;

	public GoalService(IGoalRepository goalRepository)
	{
		_goalRepository = goalRepository;
    }

	public async Task<List<Goal>> GetItemsAsync()
	{
		var list = await _goalRepository.GetItemsAsync();

		return list.Select(entity => ToGoal(entity)).OrderBy(entity => entity.TargetDate).ToList();
    }

    public async Task<Goal> GetItemAsync(Goal goal)
	{
		var entity = await _goalRepository.GetItemAsync(id);

		return ToGoal(entity);
	}
    public async Task<int> SaveItemAsync(Goal goal)
	{
		GoalEntity entity;
		if (goal.Id is null)
		{
			entity = new()
			{
				Name = goal.Name,
				Motivation = goal.Motivation,
				TargetDate = goal TargetDate,
				Progress = goal Progress
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

		return await _goalRepository.DeleteItemAsync(goal);
	}

	GoalEntity ToEntity(Goal goal)
	{
		return new GoalEntity()
		{
			Id = (int)goal.id,
			Name = goal.Name,
			Motivation = goal.Motivation,
            TargetDate = goal.TargetDate,
            Progress = goal.Progress
        }
	}

	Goal ToGoal(GoalEntity entity)
	{
		return new Goal
		{
			Id = entity.Id,
			Name = entity.Name,
			Motivation = entity.Motivation,
			TargetDate = entity.TargetDate,
			Progress = entity.Progress
		};
	}

	//Any business logic that calculated elements of Goals or GoalEntities would go here as private classes
}

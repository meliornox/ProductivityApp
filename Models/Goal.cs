namespace ProductivityApp.Models;

/// <summary>
/// Model Goal is used by the frontend to track and display Name, Motivation, TargetDate, and Progress
/// 
/// TODO Polish: Reformat Goal to have strings StartDate and EndDate and have GoalEntity have DateTimes, use GoalService to translate between them.
/// This would eliminate extra unused time information from the Goals time displayed.
/// </summary>
public class Goal
{
    public string Name { get; set; }
    public string Motivation { get; set; }
    public string TargetDate { get; set; }
    public double Progress { get; set; }
}
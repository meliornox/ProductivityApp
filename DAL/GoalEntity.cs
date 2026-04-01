using SQLite;

namespace ProductivityApp.DAL;

/// <summary>
/// GoalEntity defines the database object for Goals, and in SQLite, thereby creates the database
/// </summary>
public class GoalEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Motivation { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime TargetDate { get; set; }
}

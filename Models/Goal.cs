namespace ProductivityApp.Models;

public class Goal
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Motivation { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime TargetDate { get; set; }
    public double Progress { get; set; }
}
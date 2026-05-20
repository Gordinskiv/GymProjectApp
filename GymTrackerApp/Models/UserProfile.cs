namespace PracticaGymTracker.Models;

public class UserProfile
{
    public string Name { get; set; } = "Атлет";
    public double Height { get; set; } = 175;
    public double StartWeight { get; set; } = 61;
    public double TargetWeight { get; set; } = 75;
    public int WorkoutsPerWeek { get; set; } = 4;
    public int DailyCalories { get; set; } = 3000;
    public string? AvatarPath { get; set; }
}
namespace PracticaGymTracker.Models;

/// <summary>
/// Модель, що описує одну вправу в каталозі.
/// </summary>
public class ExerciseItem
{
    /// <summary>
    /// Назва вправи (наприклад, "Жим лежачи").
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Основний цільовий м'яз (наприклад, "Chest").
    /// </summary>
    public string TargetMuscle { get; set; }
    /// <summary>
    /// Допоміжний м'яз-синергіст (наприклад, "Triceps").
    /// </summary>
    public string SecondaryTargetMuscle { get; set; }
}
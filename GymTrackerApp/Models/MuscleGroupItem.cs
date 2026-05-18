using System.Collections.ObjectModel;
using PracticaGymTracker.ViewModels;

namespace PracticaGymTracker.Models;

/// <summary>
/// Модель для групування вправ за цільовими частинами тіла.
/// </summary>
public class MuscleGroupItem
{
    /// <summary>
    /// Назва групи м'язів для заголовка (наприклад, "ГРУДИ").
    /// </summary>
    public string GroupName { get; set; }
    /// <summary>
    /// Масив вправ, що належать до цієї групи.
    /// </summary>
    public ObservableCollection<ExerciseItem> Exercises { get; set; }
}
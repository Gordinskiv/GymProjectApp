using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using PracticaGymTracker.Models;
using PracticaGymTracker.Services;

namespace PracticaGymTracker.ViewModels;

/// <summary>
/// Відображення сторінку Тренування.
/// </summary>
public partial class WorkoutsViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<WorkoutItem> _workoutsList;
    
    [ObservableProperty]
    private string _newExerciseName = string.Empty;

    [ObservableProperty]
    private string _newWeight = string.Empty;

    [ObservableProperty]
    private string _newReps = string.Empty;
    
    private readonly JsonDataService _dataService;
    
    /// <summary>
    /// Додавання вправи, які записується в файл JSON.
    /// </summary>
    [RelayCommand]
    private void AddWorkout()
    {
        if (!string.IsNullOrWhiteSpace(NewExerciseName))
        {
            int.TryParse(NewReps, out int parsedReps);
            
            var newExercise = new WorkoutItem
            {
                ExerciseName = NewExerciseName,
                Weight = string.IsNullOrWhiteSpace(NewWeight) ? "0 кг" : NewWeight,
                Reps = parsedReps
            };
            
            WorkoutsList.Add(newExercise);
            
            _dataService.SaveWorkouts(WorkoutsList);
            
            NewExerciseName = string.Empty;
            NewWeight = string.Empty;
            NewReps = string.Empty;
        }
    }
    
    /// <summary>
    /// Конструктор, який завантажує з JSON файлу вправи.
    /// </summary>
    public WorkoutsViewModel()
    {
        _dataService = new JsonDataService();
        var loadedWorkouts = _dataService.LoadWorkouts();
        WorkoutsList = new ObservableCollection<WorkoutItem>(loadedWorkouts);
    }
    /// <summary>
    /// Видалення вибраної вправи.
    /// </summary>
    /// <param name="workout">Виділена вправа</param>
    [RelayCommand]
    private void DeleteWorkout(WorkoutItem workout)
    {
        if (workout != null && WorkoutsList.Contains(workout))
        {
            WorkoutsList.Remove(workout);
            _dataService.SaveWorkouts(WorkoutsList);
        }
    }
}
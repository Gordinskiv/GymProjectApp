using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using PracticaGymTracker.Models;
using System.Linq;
using PracticaGymTracker.Services;

namespace PracticaGymTracker.ViewModels;

/// <summary>
/// Відображення сторінку "Головна".
/// </summary>
public partial class HomeViewModel : ViewModelBase
{
    [ObservableProperty] private string _workoutsThisWeek = "0";
    [ObservableProperty] private string _maxRecord = "0 кг";
    [ObservableProperty] private string _favoriteExercise = "-";
    
    private readonly JsonDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<RecentLogItem> _recentLogs = new();
    
    public HomeViewModel()
    {
        _dataService = new JsonDataService();
        LoadActualData();
    }
    
    /// <summary>
    /// Завантаження актуальних даних з JSON файлу для подальшого відображення їх на віджетах.
    /// </summary>
    public void LoadActualData()
    {
        var allWorkouts = _dataService.LoadWorkouts();
        
        if (allWorkouts.Any())
        {
            var maxWeightValue = allWorkouts
                .Select(w => int.TryParse(new string(w.Weight.Where(char.IsDigit).ToArray()), out int weight) ? weight : 0)
                .Max();
            MaxRecord = $"{maxWeightValue} кг";
            
            FavoriteExercise = allWorkouts
                .GroupBy(w => w.ExerciseName)
                .OrderByDescending(g => g.Count())
                .First().Key;
            
            WorkoutsThisWeek = allWorkouts.Count.ToString();
        }
        var latestExercises = allWorkouts.AsEnumerable().Reverse().Take(3);
        RecentLogs.Clear();

        foreach (var workout in latestExercises)
        {
            RecentLogs.Add(new RecentLogItem
            {
                Date = "Сьогодні",
                Title = workout.ExerciseName,
                Summary = $"{workout.Weight} • {workout.Reps} повт."
            });
        }
    }
    
}
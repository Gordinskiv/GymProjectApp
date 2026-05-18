using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticaGymTracker.Models;

namespace PracticaGymTracker.ViewModels;

/// <summary>
/// ViewModel для сторінки каталогу вправ.
/// Відповідає за відображення списку доступних тренувань, згрупованих за категоріями.
/// </summary>
public partial class ExercisesCatalogViewModel : ViewModelBase
{
    /// <summary>
    /// Колекція, що містить усі групи вправ для відображення в меню.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<MuscleGroupItem> _catalog;

    /// <summary>
    /// Назва обраної вправи для відображення на головному екрані каталогу.
    /// </summary>
    [ObservableProperty] private string _selectedExerciseName = "Виберіть вправу";

    public ExercisesCatalogViewModel()
    {
        Catalog = new ObservableCollection<MuscleGroupItem>
        {
            new MuscleGroupItem
            {
                GroupName = "ГРУДНІ М'ЯЗИ",
                Exercises = new ObservableCollection<ExerciseItem>
                {
                    new ExerciseItem { Name = "Жим лежачи", TargetMuscle = "Chest", SecondaryTargetMuscle = "Triceps" },
                    new ExerciseItem { Name = "Жим на похилій лаві", TargetMuscle = "Chest", SecondaryTargetMuscle = "Triceps" },
                    new ExerciseItem { Name = "Віджимання на брусах", TargetMuscle = "Chest", SecondaryTargetMuscle = "Triceps" },
                }
            },
            new MuscleGroupItem
            {
                GroupName = "СПИНА",
                Exercises = new ObservableCollection<ExerciseItem>
                {
                    new ExerciseItem { Name = "Вертикальна тяга", TargetMuscle = "Back" },
                    new ExerciseItem { Name = "Горизонтальна тяга", TargetMuscle = "Back" },
                    new ExerciseItem { Name = "Підтягування", TargetMuscle = "Back" }
                }
            },
            new MuscleGroupItem
            {
                GroupName = "НОГИ",
                Exercises = new ObservableCollection<ExerciseItem>
                {
                    new ExerciseItem { Name = "Присід зі штангою", TargetMuscle = "Legs" },
                    new ExerciseItem { Name = "Випади", TargetMuscle = "Legs" },
                    new ExerciseItem { Name = "Жим ногами", TargetMuscle = "Legs" },
                    new ExerciseItem { Name = "Піднімання на носки", TargetMuscle = "Legs" }
                }
            }
        };
    }
}
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using PracticaGymTracker.Models;
using CommunityToolkit.Mvvm.Input;

namespace PracticaGymTracker.ViewModels;

/// <summary>
/// ViewModel для панелі управління тренера.
/// Дозволяє керувати списком клієнтів та переглядати їхній прогрес.
/// </summary>
public partial class AdminPanelViewModel : ViewModelBase
{
    /// <summary>
    /// Динамічна колекція клієнтів. 
    /// Зміни в цій колекції автоматично відображаються в інтерфейсі (UI).
    /// </summary>
    [ObservableProperty] 
    private ObservableCollection<ClientItem> _clientsList;
    
    /// <summary>
    /// Загальна кількість зареєстрованих клієнтів (для верхньої картки).
    /// </summary>
    [ObservableProperty] private string _totalClients = "24";
    /// <summary>
    /// Кількість клієнтів, які були активні сьогодні (для верхньої картки).
    /// </summary>
    [ObservableProperty] private string _activeToday = "8";

    public AdminPanelViewModel()
    {
        ClientsList = new ObservableCollection<ClientItem>
        {
            new ClientItem { Name = "Олександр В.", Goal = "Схуднення (-5 кг)", LastActive = "Онлайн", ProgressPercent = 65, StatusColor = "#00FF00" },
            new ClientItem { Name = "Марія К.", Goal = "Рельєф", LastActive = "Сьогодні, 10:15", ProgressPercent = 80, StatusColor = "#888888" },
            new ClientItem { Name = "Іван С.", Goal = "Силові показники (Жим 100)", LastActive = "Вчора", ProgressPercent = 40, StatusColor = "#888888" },
            new ClientItem { Name = "Денис П.", Goal = "Набір маси (+10 кг)", LastActive = "Онлайн", ProgressPercent = 25, StatusColor = "#00FF00" }
        };
    }
}
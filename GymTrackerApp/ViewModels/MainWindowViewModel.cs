using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PracticaGymTracker.ViewModels;

/// <summary>
/// Головний ViewModel програми. 
/// Відповідає за життєвий цикл застосунку, навігацію між сторінками 
/// та відображення бокового меню.
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    /// <summary>
    /// Коректна вибрана сторінка.
    /// </summary>
    [ObservableProperty]
    private object _currentPage;
    
    /// <summary>
    /// Вказує, чи має відображатися бокове навігаційне меню.
    /// Доступне лише після успішної авторизації користувача.
    /// </summary>
    [ObservableProperty]    
    private bool _isMenuVisible = false; 
    /// <summary>
    /// Вказує, чи має відображатися секретна кнопка для ролі "Тренер".
    /// </summary>
    [ObservableProperty] private bool _isAdminPanelVisible = false;

    public MainWindowViewModel()
    {
        ShowLogin();
    }
    /// <summary>
    /// Відображає сторінку авторизації та приховує головне меню.
    /// </summary>
    public void ShowLogin()
    {
        IsMenuVisible = false;
        CurrentPage = new LoginViewModel(PerformLogin); 
    }
    /// <summary>
    /// Обробляє успішний вхід у систему.
    /// </summary>
    /// <param name="isAdmin">Передає true, якщо користувач увійшов з правами тренера.</param>
    private void PerformLogin(bool isAdmin = false)
    {
        IsAdminPanelVisible = isAdmin;
        IsMenuVisible = true;
        ShowHome();
    }
    /// <summary>
    /// Відображає сторінку логіна.
    /// </summary>
    [RelayCommand]
    private void Logout()
    {
        ShowLogin();
    }
    /// <summary>
    /// Виконує вихід із системи, очищає поточну сесію та повертає на екран входу.
    /// </summary>
    [RelayCommand]
    private void Exit()
    {
        ShowLogin();
    }
    /// <summary>
    /// Відображення сторінку реєстрації.
    /// </summary>
    [RelayCommand]
    public void ShowRegister()
    {
        IsMenuVisible = false;
        CurrentPage = new RegisterViewModel(PerformLogin, ShowLogin);
    }
    /// <summary>
    /// Відображення сторінку "Головна".
    /// </summary>
    [RelayCommand]
    private void ShowHome()
    {
        var homeVM = new HomeViewModel();
        CurrentPage = homeVM; 
    }
    /// <summary>
    /// Відображення сторінку "Тренування".
    /// </summary>
    [RelayCommand]
    private void ShowWorkouts() => CurrentPage = new WorkoutsViewModel();
    
    /// <summary>
    /// Відображення сторінку "Налаштування".
    /// </summary>
    [RelayCommand]
    private void ShowSettings() => CurrentPage = new SettingsViewModel();
    
    /// <summary>
    /// Відображення сторінку "Аналітика".
    /// </summary>
    [RelayCommand]
    private void ShowAnalytics() => CurrentPage = new AnalyticsViewModel();
    
    /// <summary>
    /// Відображення сторінку "Досягнення".
    /// </summary>
    [RelayCommand]
    private void ShowAchievements() => CurrentPage = new AchievementsViewModel();
    
    /// <summary>
    /// Відображення сторінку "Каталог Вправ".
    /// </summary>
    [RelayCommand]
    private void ShowExercisesCatalog() => CurrentPage = new ExercisesCatalogViewModel();
    
    /// <summary>
    /// Відображення сторінку "Панель Тренувань".
    /// </summary>
    [RelayCommand]
    private void ShowAdminPanel() => CurrentPage = new AdminPanelViewModel();
}

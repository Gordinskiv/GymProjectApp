using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PracticaGymTracker.ViewModels;

public partial class RegisterViewModel : ViewModelBase
{
    private readonly Action<bool> _onRegisterSuccess;
    private readonly Action _onBackToLogin;
    
    /// <summary>
    /// Прапорець вибору ролі "Спортсмен".
    /// </summary>
    [ObservableProperty] private bool _isUserRole = true;
    /// <summary>
    /// Прапорець вибору ролі "Тренер".
    /// </summary>
    [ObservableProperty] private bool _isAdminRole = false;

    public RegisterViewModel(Action<bool> onRegisterSuccess, Action onBackToLogin)
    {
        _onRegisterSuccess = onRegisterSuccess;
        _onBackToLogin = onBackToLogin;
    }
    /// <summary>
    /// Реєструє акаунт та передає обрану роль у головне вікно.
    /// </summary>
    [RelayCommand]
    private void Register()
    {
        _onRegisterSuccess?.Invoke(IsAdminRole);
    }
    /// <summary>
    /// Повертає на екран входу без реєстрації.
    /// </summary>
    [RelayCommand]
    private void GoToLogin()
    {
        _onBackToLogin?.Invoke();
    }
}
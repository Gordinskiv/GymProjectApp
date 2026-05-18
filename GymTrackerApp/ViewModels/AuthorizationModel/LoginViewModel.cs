using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PracticaGymTracker.ViewModels;

/// <summary>
/// ViewModel для екрана авторизації.
/// </summary>
public partial class LoginViewModel : ViewModelBase
{
    private readonly Action<bool> _onLoginSuccess;
    
    [ObservableProperty]
    private bool _isAdminRole = false;

    public LoginViewModel(Action<bool> onLoginSuccess)
    {
        _onLoginSuccess = onLoginSuccess;
    }
    /// <summary>
    /// Виконує вхід користувача в систему.
    /// </summary>
    [RelayCommand]
    private void Login()
    {
        _onLoginSuccess?.Invoke(IsAdminRole);
    }
}
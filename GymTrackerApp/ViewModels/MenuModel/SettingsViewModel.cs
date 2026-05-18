using System;
using CommunityToolkit.Mvvm.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PracticaGymTracker.ViewModels;

/// <summary>
/// Відображення сторінки налаштування.
/// </summary>
public partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _currentLanguage = "ua";
    
    /// <summary>
    /// Змінює мову на протилежну при викликанню методу.
    /// З UA на EN(українську на англійську) і протилежно.
    /// </summary>
    [RelayCommand]
    private void ToggleLanguage()
    {
        CurrentLanguage = CurrentLanguage == "ua" ? "en" : "ua";

        ChangeLanguageDictionaries(CurrentLanguage);
    }
    /// <summary>
    /// Змінює Resource який буде використовуватися.
    /// </summary>
    /// <param name="langCode">Вибрана мова</param>
    private void ChangeLanguageDictionaries(string langCode)
    {
        var app = Application.Current;
        if (app == null) return;
        
        string path = $"avares://GymTrackerApp/Resources/Strings.{langCode}.axaml";
        
        try
        {
            var newDict = (ResourceDictionary)Avalonia.Markup.Xaml.AvaloniaXamlLoader.Load(new Uri(path));

            app.Resources.MergedDictionaries.Clear();
            app.Resources.MergedDictionaries.Add(newDict);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка завантаження мови: {ex.Message}");
        }
    }
    [ObservableProperty]
    private string _currentTheme = "DARK";
    
    /// <summary>
    /// Змінює тему на протилежну при викликанню методу.
    /// З DARK на LIGHT(темну на світлу) і протилежно.
    /// </summary>
    [RelayCommand]
    private void ToggleTheme()
    {
        var app = Application.Current;
        if (app == null) return;
        
        if (app.ActualThemeVariant == ThemeVariant.Dark)
        {
            app.RequestedThemeVariant = ThemeVariant.Light;
            CurrentTheme = "LIGHT";
        }
        else
        {
            app.RequestedThemeVariant = ThemeVariant.Dark;
            CurrentTheme = "DARK";
        }
    }
}
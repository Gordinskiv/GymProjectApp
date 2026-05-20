using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticaGymTracker.Models;
using PracticaGymTracker.Services;
using Avalonia.Media.Imaging; 
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace PracticaGymTracker.ViewModels;

public partial class ProfileViewModel : ViewModelBase
{
    private readonly JsonDataService _dataService;
    private readonly AnalyticsViewModel _calculateProgress;
    private readonly string _avatarsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProfileAvatars");
    
    [ObservableProperty] private string _userName;
    [ObservableProperty] private string _heightString;
    [ObservableProperty] private string _startWeightString;
    [ObservableProperty] private string _targetWeightString;
    [ObservableProperty] private string _workoutsPerWeekString;
    [ObservableProperty] private string _dailyCaloriesString;
    
    [ObservableProperty] private string _saveMessage;
    
    private string? _selectedAvatarPath;
    [ObservableProperty] private Bitmap? _avatarBitmap;

    public ProfileViewModel()
    {
        _dataService = new JsonDataService();
        if (!Directory.Exists(_avatarsFolder)) Directory.CreateDirectory(_avatarsFolder);
        LoadProfileData();
    }

    private void LoadProfileData()
    {
        var profile = _dataService.LoadProfile();
        UserName = profile.Name;
        HeightString = profile.Height.ToString();
        StartWeightString = profile.StartWeight.ToString();
        TargetWeightString = profile.TargetWeight.ToString();
        WorkoutsPerWeekString = profile.WorkoutsPerWeek.ToString();
        DailyCaloriesString = profile.DailyCalories.ToString();
        _selectedAvatarPath = profile.AvatarPath;
        if (!string.IsNullOrEmpty(_selectedAvatarPath) && File.Exists(_selectedAvatarPath))
        {
            try
            {
                AvatarBitmap = new Bitmap(_selectedAvatarPath);
            }
            catch
            {
                throw new AbandonedMutexException("Файл пошкоджений");
            }
        }
    }
    [RelayCommand]
    private async Task SelectNewAvatar(Visual thisView)
    {
        var topLevel = TopLevel.GetTopLevel(thisView);
        if (topLevel == null) return;
        
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Оберіть фото профілю",
            AllowMultiple = false,
            FileTypeFilter = new[] { FilePickerFileTypes.ImageAll }
        });
        if (files.Count >= 1)
        {
            var file = files[0];
            string originalPath = file.Path.LocalPath;
            
            AvatarBitmap = new Bitmap(originalPath);
            _selectedAvatarPath = originalPath;
        }
    }

    [RelayCommand]
    private void SaveProfile()
    {
        if (double.TryParse(HeightString, out double h) &&
            double.TryParse(StartWeightString, out double sw) &&
            double.TryParse(TargetWeightString, out double tw) &&
            int.TryParse(WorkoutsPerWeekString, out int workouts) &&
            int.TryParse(DailyCaloriesString, out int calories))
        {
            string finalAvatarPath = _selectedAvatarPath;

            if (_selectedAvatarPath != null && File.Exists(_selectedAvatarPath))
            {
                if (!_selectedAvatarPath.StartsWith(_avatarsFolder))
                {
                    string fileName = Path.GetFileName(_selectedAvatarPath);
                    string uniqueFileName = $"avatar_{DateTime.Now.Ticks}_{fileName}";
                    finalAvatarPath = Path.Combine(_avatarsFolder, uniqueFileName);

                    try
                    {
                        File.Copy(_selectedAvatarPath, finalAvatarPath, true);
                    }
                    catch
                    {
                        throw new AbandonedMutexException();
                    }
                }
            }
            var profile = new UserProfile
            {
                Name = UserName,
                Height = h,
                StartWeight = sw,
                TargetWeight = tw,
                WorkoutsPerWeek = workouts,
                DailyCalories = calories,
                AvatarPath = finalAvatarPath
            };
                
            _dataService.SaveProfile(profile);
            SaveMessage = "Дані успішно збережено!";
            _selectedAvatarPath = finalAvatarPath;
        }
        else
        {
            SaveMessage = "Помилка! Введіть коректні числа.";
        }
    }
}
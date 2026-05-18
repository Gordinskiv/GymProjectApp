using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticaGymTracker.Models;
using PracticaGymTracker.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace PracticaGymTracker.ViewModels;

public partial class AnalyticsViewModel : ViewModelBase
{
    private readonly JsonDataService _dataService;

    [ObservableProperty] private ObservableCollection<BodyMeasurementItem> _measurementsHistory;
    [ObservableProperty] private double _currentWeight;
    [ObservableProperty] private double _progressPercentage;
    [ObservableProperty]
    private ISeries[] _weightSeries;
    [ObservableProperty]
    private Axis[] _xAxes;
    
    [ObservableProperty] 
    private BodyMeasurementItem _selectedMeasurement;
    [ObservableProperty] private string _newWeight;
    [ObservableProperty] private string _newChest;
    [ObservableProperty] private string _newBiceps;
    [ObservableProperty] private string _newWaist;

    public AnalyticsViewModel()
    {
        _dataService = new JsonDataService();
        LoadData();
    }

    private void LoadData()
    {
        var data = _dataService.LoadMeasurements();
        MeasurementsHistory = new ObservableCollection<BodyMeasurementItem>(data);

        CalculateProgress();
        UpdateChart();
    }

    private void CalculateProgress()
    {
        double startWeight = 61.0;
        double targetWeight = 75.0;

        if (MeasurementsHistory.Any())
        {
            CurrentWeight = MeasurementsHistory.Last().Weight;
        }
        else
        {
            CurrentWeight = startWeight;
        }

        if (CurrentWeight >= targetWeight)
        {
            ProgressPercentage = 100;
        }
        else if (CurrentWeight <= startWeight)
        {
            ProgressPercentage = 0;
        }
        else
        {
            ProgressPercentage = ((CurrentWeight - startWeight) / (targetWeight - startWeight)) * 100;
        }
    }

    [RelayCommand]
    private void AddMeasurement()
    {
        if (double.TryParse(NewWeight, out double parsedWeight))
        {
            double.TryParse(NewChest, out double parsedChest);
            double.TryParse(NewBiceps, out double parsedBiceps);
            double.TryParse(NewWaist, out double parsedWaist);

            var newItem = new BodyMeasurementItem
            {
                Date = DateTime.Now,
                Weight = parsedWeight,
                Chest = parsedChest,
                Biceps = parsedBiceps,
                Waist = parsedWaist
            };
            MeasurementsHistory.Add(newItem);
            _dataService.SaveMeasurements(MeasurementsHistory.ToList());
            CalculateProgress();
            UpdateChart();
            
            NewWeight = string.Empty;
            NewChest = string.Empty;
            NewBiceps = string.Empty;
            NewWaist = string.Empty;
        }
    }

    [RelayCommand]
    private void DeleteMeasurement()
    {
        if (SelectedMeasurement != null)
        {
            MeasurementsHistory.Remove(SelectedMeasurement);
            _dataService.SaveMeasurements(MeasurementsHistory.ToList());
            CalculateProgress();
            UpdateChart();
        }
    }
    private void UpdateChart()
    {
        var weights = MeasurementsHistory.Select(m => m.Weight).ToArray();
        var dates = MeasurementsHistory.Select(m => m.DisplayDate).ToArray();
        
        WeightSeries = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = weights,
                Name = "Вага (кг)",
                Stroke = new SolidColorPaint(SKColors.DarkOrange) { StrokeThickness = 3 },
                GeometryFill = new SolidColorPaint(SKColors.DarkOrange),
                GeometryStroke = new SolidColorPaint(SKColors.DarkOrange) { StrokeThickness = 3 },
                Fill = null,
                LineSmoothness = 0.5
            }
        };
        XAxes = new Axis[]
        {
            new Axis
            {
                Labels = dates,
                LabelsPaint = new SolidColorPaint(SKColors.LightGray),
                TextSize = 14
            }
        };
    }
}

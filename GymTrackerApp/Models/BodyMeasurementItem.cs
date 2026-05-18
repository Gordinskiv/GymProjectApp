using System;

namespace PracticaGymTracker.Models;

public class BodyMeasurementItem
{
    public DateTime Date { get; set; }
    public double Weight { get; set; }
    public double Chest { get; set; }
    public double Biceps { get; set; }
    public double Waist { get; set; }
    
    public string DisplayDate => Date.ToString("dd.MM.yy");
}
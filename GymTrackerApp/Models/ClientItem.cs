namespace PracticaGymTracker.Models;

/// <summary>
/// Модель, що описує клієнта.
/// </summary>
public class ClientItem
{
    /// <summary>
    /// Повне ім'я або нікнейм користувача.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Мета користувача.
    /// </summary>
    public string Goal { get; set; }      
    /// <summary>
    /// Остання активність користувача.
    /// </summary>
    public string LastActive { get; set; }  
    /// <summary>
    /// Поточний прогрес виконання цілі у відсотках.
    /// </summary>
    public int ProgressPercent { get; set; }
    /// <summary>
    /// Колір, яким буде відображатися користувач(онлайн/офлайн).
    /// </summary>
    public string StatusColor { get; set; }
}
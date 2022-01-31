namespace MyCosts.API.Data.Models;

/// <summary>
///     Кассовый чек в магазине
/// </summary>
[Table("Bills")]
public class Bill
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    /// <summary>
    ///     Дата покупки
    /// </summary>
    [Required]
    public DateTime Date { get; set; }

    /// <summary>
    ///     Название магазина
    /// </summary>
    [MaxLength(50)]
    public string? Store { get; set; }

    public List<Cost>? Costs { get; set; }
}

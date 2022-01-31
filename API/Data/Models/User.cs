namespace MyCosts.API.Data.Models;

[Table("Users")]
public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string Password { get; set; } = string.Empty;

    public List<Bill>? Bills { get; set; }
    public List<Cost>? Costs { get; set; }
}

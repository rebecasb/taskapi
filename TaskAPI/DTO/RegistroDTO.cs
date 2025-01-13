using System.ComponentModel.DataAnnotations;

namespace TaskAPI.DTO;

public class RegistroDTO
{
    [Required]
    [StringLength(50)]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(12, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}

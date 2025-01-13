using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace TaskAPI.Models;

public class Usuario
{
    public long Id { get; set; }
    [Required]
    public string NombreUsuario { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
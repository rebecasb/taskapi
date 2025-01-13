using System.ComponentModel.DataAnnotations;
using TaskAPI.Models;

namespace TaskAPI.DTO;

public class TareaDTO
{
    [Required]
    [StringLength(150)]
    public string Titulo { get; set; } = string.Empty;
    
    [StringLength(2000)]
    public string Descripcion { get; set; } = string.Empty;
    
    [Required]
    public EstadoTarea Estado { get; set; }
    
    [Required]
    public DateTime FechaVencimiento { get; set; }

}

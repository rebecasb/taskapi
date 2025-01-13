using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace TaskAPI.Models;

public class Tarea
{
        public long Id { get; set; }
        [Required]
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        [Required]
        public EstadoTarea Estado { get; set; } = EstadoTarea.Pendiente;
        [Required]
        public DateTime FechaVencimiento { get; set; }
        [ForeignKey("User")]
        public long IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = null!;
}

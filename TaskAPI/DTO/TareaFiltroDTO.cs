using TaskAPI.Models;

namespace TaskAPI.DTO;

public class TareaFiltroDTO
{
    public EstadoTarea? Estado { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public int Pagina { get; set; } = 1;
    public int PaginaLimite { get; set; } = 15;

}

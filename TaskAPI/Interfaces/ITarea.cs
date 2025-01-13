using Microsoft.AspNetCore.Mvc;
using TaskAPI.Models;

namespace TaskAPI.Interfaces;

public interface ITarea
{
    Task<IEnumerable<Tarea>> GetTareasUsuarioAsync(int idUsuario, EstadoTarea? estado = null,
            DateTime? fechaVencimiento = null, int pagina = 1, int paginaLimite = 15);
    Task<Tarea?> GetByIdAsync(int id, int idUsuario);
    Task<Tarea> CreateAsync(Tarea tarea);
    Task UpdateAsync(Tarea tarea);
    Task DeleteAsync(Tarea tarea);
    Task<int> GetTotalTareasAsync(int idUsuario, EstadoTarea? estado = null, DateTime? fechaVencimiento = null);

}

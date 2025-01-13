using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TaskAPI.Data;
using TaskAPI.Interfaces;
using TaskAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskAPI.Services;

public class TareaService : ITarea
{
    private readonly TaskContext _dbcontext;

    public TareaService(TaskContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Tarea> CreateAsync(Tarea tarea)
    {
        await _dbcontext.Tareas.AddAsync(tarea);
        await _dbcontext.SaveChangesAsync();
        return tarea;
    }

    public async Task DeleteAsync(Tarea tarea)
    {
        _dbcontext.Tareas.Remove(tarea);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<Tarea?> GetByIdAsync(int id, int idUsuario)
    {
        return await _dbcontext.Tareas
                .FirstOrDefaultAsync(t => t.Id == id && t.IdUsuario == idUsuario);
    }

    public async Task<IEnumerable<Tarea>> GetTareasUsuarioAsync(int idUsuario, EstadoTarea? estado = null, DateTime? fechaVencimiento = null, int pagina = 1, int paginaLimite = 15)
    {
        var qry = _dbcontext.Tareas.Where(t => t.IdUsuario == idUsuario);

        if (estado.HasValue)
            qry = qry.Where(t => t.Estado == estado.Value);

        if (fechaVencimiento.HasValue)
            qry = qry.Where(t => t.FechaVencimiento.Date == fechaVencimiento.Value.Date);

        return await qry
            .Skip((pagina - 1) * paginaLimite)
            .Take(paginaLimite)
            .ToListAsync();

    }

    public async Task UpdateAsync(Tarea tarea)
    {
        _dbcontext.Tareas.Update(tarea);
        await _dbcontext.SaveChangesAsync();
    }


    public async Task<int> GetTotalTareasAsync(int idUsuario, EstadoTarea? estado = null, DateTime? fechaVencimiento = null)
    {
        var qry = _dbcontext.Tareas.Where(t => t.IdUsuario == idUsuario);

        if (estado.HasValue)
            qry = qry.Where(t => t.Estado == estado.Value);

        if (fechaVencimiento.HasValue)
            qry = qry.Where(t => t.FechaVencimiento.Date == fechaVencimiento.Value.Date);

        return await qry.CountAsync();
    }

}
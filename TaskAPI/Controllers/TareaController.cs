using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskAPI.Data;
using TaskAPI.DTO;
using TaskAPI.Models;
using TaskAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskAPI.Controllers;

[Authorize]
[ApiController]
[Route("tarea")]
public class TareaController : ControllerBase
{
    private readonly ITarea _tareaService;

    public TareaController(ITarea tareaService)
    {
        _tareaService = tareaService;
    }

    [HttpPost]
    public async Task<ActionResult<Tarea>> Crear([FromBody] TareaDTO tareaDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var IdUsuario))
        {
            return Unauthorized("Usuario no autenticado");
        }

        var tarea = new Models.Tarea
        {
            Titulo = tareaDTO.Titulo,
            Descripcion = tareaDTO.Descripcion,
            Estado = tareaDTO.Estado,
            FechaVencimiento = tareaDTO.FechaVencimiento,
            IdUsuario = IdUsuario
        };

        try
        {
            var nuevaTarea = await _tareaService.CreateAsync(tarea);
            return CreatedAtAction(nameof(GetTarea), new { id = nuevaTarea.Id }, nuevaTarea);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error creando la tarea: {ex.Message}");
        }

    }

    [HttpGet]
    public async Task<IActionResult> GetFiltro([FromQuery] string? estado, [FromQuery] DateTime? fechaVencimiento, [FromQuery] int pagina = 1, [FromQuery] int paginaLimite = 15)
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var idUsuario))
        {
            return Unauthorized("Usuario no autenticado");
        }

        EstadoTarea? estadoConvertido = null;
        if (!string.IsNullOrEmpty(estado))
        {
            if (Enum.TryParse<EstadoTarea>(estado, true, out var estadoResultado))
            {
                estadoConvertido = estadoResultado;
            }
            else
            {
                return BadRequest($"El valor '{estado}' no es válido para el estado.");
            }
        }


        try
        {
            var tareas = await _tareaService.GetTareasUsuarioAsync(
                idUsuario,
                estadoConvertido,
                fechaVencimiento,
                pagina,
                paginaLimite);

            var totalTareas = await _tareaService.GetTotalTareasAsync(idUsuario, estadoConvertido, fechaVencimiento);
            var totalPaginas = (int)Math.Ceiling(totalTareas / (double)paginaLimite);

            Response.Headers.Add("X-Total-Count", totalTareas.ToString());
            Response.Headers.Add("X-Total-Pages", totalPaginas.ToString());

            return Ok(tareas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error obteniendo las tareas: {ex.Message}");
        }


    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetTarea(int id)
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var idUsuario))
        {
            return Unauthorized("Usuario no autenticado");
        }

        var tarea = await _tareaService.GetByIdAsync(id, idUsuario);

        if (tarea == null)
        {
            return NotFound("Tarea no encontrada o no pertenece al usuario.");
        }

        return Ok(tarea);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TareaDTO tareaDTO)
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var idUsuario))
        {
            return Unauthorized("Usuario no autenticado");
        }

        try
        {
            var existeTarea = await _tareaService.GetByIdAsync(id, idUsuario);
            if (existeTarea == null || existeTarea.IdUsuario != idUsuario)
            {
                return NotFound("La tarea no existe o no pertenece al usuario.");
            }

            existeTarea.Titulo = tareaDTO.Titulo;
            existeTarea.Descripcion = tareaDTO.Descripcion;
            existeTarea.Estado = tareaDTO.Estado;
            existeTarea.FechaVencimiento = tareaDTO.FechaVencimiento;

            await _tareaService.UpdateAsync(existeTarea);

            return Ok(new { message = "Tarea actualizada correctamente", task = existeTarea });

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al actualizar la tarea: {ex.Message}");
        }

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var idUsuario))
        {
            return Unauthorized("Usuario no autenticado");
        }

        var tarea = await _tareaService.GetByIdAsync(id, idUsuario);
        if (tarea == null || tarea.IdUsuario != idUsuario)
        {
            return NotFound("Tarea no encontrada o no pertenece al usuario.");
        }

        try
        {
            await _tareaService.DeleteAsync(tarea);
            return Ok(new { message = "Tarea eliminada correctamente" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al eliminar la tarea: {ex.Message}");
        }

    }

}
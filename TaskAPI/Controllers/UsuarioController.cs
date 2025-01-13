using Microsoft.AspNetCore.Mvc;
using System;
using TaskAPI.Data;
using TaskAPI.DTO;
using TaskAPI.Models;
using TaskAPI.Interfaces;

namespace TaskAPI.Controllers;

[ApiController]
[Route("usuario")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuario _usuarioService;
    private readonly IAuth _authService;

    public UsuarioController(IUsuario usuarioService, IAuth authService)
    {
        _usuarioService = usuarioService;
        _authService = authService;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar(RegistroDTO registroDTO)
    {
        if (await _usuarioService.ExisteEmailsAsync(registroDTO.Email))
            return BadRequest("El correo electrónico ya está registrado.");

        if (await _usuarioService.ExisteNombreUsuarioAsync(registroDTO.NombreUsuario))
            return BadRequest("El nombre de usuario ya está registrado.");

        var nuevoUsuario = new Usuario
        {
            NombreUsuario = registroDTO.NombreUsuario,
            Email = registroDTO.Email,
            PasswordHash = _authService.HashPassword(registroDTO.Password)
        };

        await _usuarioService.CreateAsync(nuevoUsuario);

        var token = _authService.GenerarJwtToken(nuevoUsuario);
        return Ok(new { Token = token });

    }

    [HttpPost("autenticar")]
    public async Task<ActionResult> Autenticar(LoginDto loginDto)
    {
        var usuario = await _usuarioService.GetByEmailAsync(loginDto.Email);
        if (usuario == null)
            return Unauthorized("Credenciales inválidas.");

        if (!_authService.VerificarPassword(loginDto.Password, usuario.PasswordHash))
            return Unauthorized("Credenciales inválidas.");

        var token = _authService.GenerarJwtToken(usuario);
        return Ok(new { Token = token });

    }

}

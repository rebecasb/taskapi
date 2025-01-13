using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Data;
using TaskAPI.Interfaces;
using TaskAPI.Models;

namespace TaskAPI.Services;

public class UsuarioService : IUsuario
{
    private readonly TaskContext _dbcontext;

    public UsuarioService(TaskContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        await _dbcontext.Usuarios.AddAsync(usuario);
        await _dbcontext.SaveChangesAsync();
        return usuario;
    }

    public async Task<bool> ExisteEmailsAsync(string email)
    {
        return await _dbcontext.Usuarios.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario)
    {
        return await _dbcontext.Usuarios.AnyAsync(u => u.NombreUsuario == nombreUsuario);
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _dbcontext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario> GetByIdAsync(int id)
    {
        return await _dbcontext.Usuarios.FindAsync(id);
    }
}

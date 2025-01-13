using Microsoft.AspNetCore.Mvc;
using TaskAPI.Models;

namespace TaskAPI.Interfaces;

public interface IUsuario
{
    Task<Usuario> GetByIdAsync(int id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<bool> ExisteEmailsAsync(string email);
    Task<bool> ExisteNombreUsuarioAsync(string username);
    Task<Usuario> CreateAsync(Usuario user);

}

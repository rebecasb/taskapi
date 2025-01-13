using TaskAPI.Models;

namespace TaskAPI.Interfaces;

public interface IAuth
{
    string GenerarJwtToken(Usuario user);
    string HashPassword(string password);
    bool VerificarPassword(string password, string hash);
}

using ApiToDoList.Models;

namespace ApiToDoList.Services.Role;

public interface IRoleService
{
    /// <summary>
    /// Recuperer le role de l'utilisateur
    /// </summary>
    /// <param name="_idUtilisateur">Id de l'utilisateur</param>
    /// <returns>Le role</returns>
    Task<Models.Role> RoleUtilisateurAsync(int _idUtilisateur);

    /// <summary>
    /// Regarde si le role existe
    /// </summary>
    /// <param name="_idRole">Id du role</param>
    /// <returns>True => OK / False => Non</returns>
    Task<bool> ExisteAsync(int _idRole);

    /// <summary>
    /// Lister tout les rôles
    /// </summary>
    /// <returns>Liste des rôles</returns>
    Task<List<Models.Role>> ListerAsync();
}

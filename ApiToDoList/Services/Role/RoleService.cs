using ApiToDoList.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiToDoList.Services.Role;

public class RoleService : IRoleService
{
    public TrelloContext Context { get; }

    public RoleService(TrelloContext _context)
    {
        Context = _context;
    }

    public async Task<bool> ExisteAsync(int _idRole)
    {
        try
        {
            return await Context.Roles.AnyAsync(x => x.Id == _idRole);
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<Models.Role>?> ListerAsync()
    {
        try
        {
            return await Context.Roles.OrderBy(x => x.Nom).ToListAsync();
        }
        catch
        {
            return null;
        }
    }

    public Task<Models.Role> RoleUtilisateurAsync(int _idUtilisateur)
    {
        throw new NotImplementedException();
    }
}

using ApiToDoList.ModelImport;
using ApiToDoList.Models;

namespace ApiToDoList.Services.Utilisateur;

public sealed class UtilisateurService: IUtilisateurService
{
    public TrelloContext Context { get; }

    public UtilisateurService(TrelloContext _context)
    {
        Context = _context;
    }

    public async Task<int> AjouterAsync(UtilisateurImport _utilisateurImport)
    {
        Models.Utilisateur utilisateur = new()
        {
            Nom = _utilisateurImport.Nom,
            Prenom = _utilisateurImport.Prenom,
            IdRole = _utilisateurImport.IdRole.HasValue ? _utilisateurImport.IdRole.Value : 2
        };

        await Context.Utilisateurs.AddAsync(utilisateur);
        await Context.SaveChangesAsync();

        return utilisateur.Id;
    }
}

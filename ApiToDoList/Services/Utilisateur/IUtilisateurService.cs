using ApiToDoList.ModelImport;

namespace ApiToDoList.Services.Utilisateur;

public interface IUtilisateurService
{
    /// <summary>
    /// Ajout d'un nouvelle utilisateur
    /// </summary>
    /// <param name="_utilisateurImport"></param>
    /// <returns>Id de l'utilisateur ajouté</returns>
    Task<int> AjouterAsync(UtilisateurImport _utilisateurImport);
}

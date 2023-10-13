using ApiToDoList.ModelImport;
using FluentValidation;

namespace ApiToDoList.Validators;

public class UtilisateurImportValidator: AbstractValidator<UtilisateurImport>
{
    public UtilisateurImportValidator()
    {
        RuleFor(x => x.Prenom).NotEmpty();
        RuleFor(x => x.Nom).NotEmpty();
        RuleFor(x => x.Mail).EmailAddress();
    }
}

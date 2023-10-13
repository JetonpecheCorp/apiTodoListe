using ApiToDoList.ModelExport;
using FluentValidation.Results;
using System.Security.Cryptography;

namespace ApiToDoList.Static;

public static class Outil
{
    public static RSA Rsa { get; set; }

    public static IResult ListerErreur(ValidationResult _validator)
    {
        return Results.UnprocessableEntity(_validator.Errors.Select(x => new ErreurExport
        {
            Message = x.ErrorMessage,
            NomPropriete = x.PropertyName
        }));
    }
}

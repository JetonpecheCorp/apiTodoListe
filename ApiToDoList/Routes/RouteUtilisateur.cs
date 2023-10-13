using ApiToDoList.ModelExport;
using ApiToDoList.ModelImport;
using ApiToDoList.Services;
using ApiToDoList.Static;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ApiToDoList.Routes;

public static class RouteUtilisateur
{
    public static RouteGroupBuilder AjouterRouteUtilisateur(this RouteGroupBuilder builder)
    {
        builder.WithOpenApi();

        builder.MapGet("ajouter", AjouterAsync)
            .WithDescription("Ajouter un nouvelle utilisateur");

        return builder;
    }

    async static Task<IResult> AjouterAsync(UtilisateurImport _utilisateurImport,
                                            [FromServices] JwtService _jwtService, 
                                            [FromServices] IValidator<UtilisateurImport> _validator)
    {
        var validator = _validator.Validate(_utilisateurImport);

        if(!validator.IsValid)
            return Outil.ListerErreur(validator);



         return Results.Ok();
    }
}

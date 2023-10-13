using ApiToDoList.Static;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ApiToDoList.Services;

public sealed class JwtService
{
    public string Generer()
    {
        var gestionnaireJwt = new JsonWebTokenHandler();

        // permet de signer le JWT
        var cle = new RsaSecurityKey(Outil.Rsa);

        // creation du JWT
        // par defaut dure 1 heure
        string jwt = gestionnaireJwt.CreateToken(new SecurityTokenDescriptor
        {
            // informations ajouter dans le JWT
            Subject = new ClaimsIdentity(new[]
            {
                // ajout d'un role (un ou plusieurs)
                new Claim(ClaimTypes.Role, "Admin")
            }),

            // OBLIGATOIRE => qui est l'émeteur
            // en général mettre URL
            Issuer = "salut",

            SigningCredentials = new SigningCredentials(cle, SecurityAlgorithms.RsaSha256)
        });

        return jwt;
    }
}

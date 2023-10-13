using ApiToDoList.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiToDoList.Routes;

public static class RouteJwt
{
    public static RouteGroupBuilder AjouterRouteJwt(this RouteGroupBuilder builder)
    {
        builder.WithOpenApi();

        builder.MapGet("generer", ([FromServices] JwtService _jwtServ) => _jwtServ.Generer());

        return builder;
    }
}

using ApiToDoList.Models;
using ApiToDoList.Routes;
using ApiToDoList.Services;
using ApiToDoList.Static;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

RSA rsa = RSA.Create();

// creer la cle une seule fois
if (!File.Exists("cleRSA/cle.bin"))
{
    // cree un fichier bin pour signer le JWT
    var clePriver = rsa.ExportRSAPrivateKey();
    File.WriteAllBytes("cleRSA/cle.bin", clePriver);
}

// recupere la clé
rsa.ImportRSAPrivateKey(File.ReadAllBytes("cleRSA/cle.bin"), out _);

Outil.Rsa = rsa;

// permet de savoir si on a le bon role pour pouvoir y acceder
// nom => a donner dans .RequireAuthorization("nom")
builder.Services.AddAuthorizationBuilder()
  .AddPolicy("admin", policy => policy.RequireRole("admin"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        // se qu'on veut valider ou non
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    // permet de valider le chiffrement du JWT en definissant la cle utilisé
                    option.Configuration = new OpenIdConnectConfiguration
                    {
                        SigningKeys = { new RsaSecurityKey(rsa) }
                    };

                    // pour avoir les clé valeur normal comme dans les claims
                    // par defaut ajouter des Uri pour certain truc comme le "sub"
                    option.MapInboundClaims = false;
                });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    // genere un XML et permet de voir le sumary dans swagger pour chaque fonctions dans le controller
    string xmlNomFichier = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlNomFichier));

    // ajout d'une option pour mettre le token en mode Bearer
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        // ou le trouver
        In = ParameterLocation.Header,
        Description = "Token",

        // nom dans le header
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",

        // JWT de type Bearer
        Scheme = "Bearer"
    });

    // verouiller les routes qui require le JWT
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddCors(x => x.AddDefaultPolicy(y => y.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Recupere tout les validators (classe qui ont AbstractValidator) pour pouvoir les utiliser
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddDbContext<TrelloContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("defaut")));

builder.Services.AddTransient<JwtService>();

var app = builder.Build();

app.UseCors();

// l'ordre est important
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.DefaultModelsExpandDepth(-1));
}

app.MapGroup("/utilisateur").AjouterRouteUtilisateur();
app.MapGroup("/jwt").AjouterRouteJwt();

app.Run();

// Scaffold-DbContext "Data Source=DESKTOP-U41J905\SQLEXPRESS;Initial Catalog=Trello;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
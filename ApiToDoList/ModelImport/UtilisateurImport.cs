﻿namespace ApiToDoList.ModelImport;

public sealed class UtilisateurImport
{
    public int? IdRole { get; set; }
    public string Nom { get; set; } = null!;
    public string Prenom { get; set; } = null!;
    public string Mail { get; set; } = null!;
}

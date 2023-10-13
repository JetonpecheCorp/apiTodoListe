using System;
using System.Collections.Generic;

namespace ApiToDoList.Models;

public partial class TodoListe
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Todo> Todos { get; set; } = new List<Todo>();

    public virtual ICollection<Utilisateur> IdUtilisateurs { get; set; } = new List<Utilisateur>();
}

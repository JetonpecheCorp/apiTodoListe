using System;
using System.Collections.Generic;

namespace ApiToDoList.Models;

public partial class Todo
{
    public int Id { get; set; }

    public int IdTodo { get; set; }

    public string Nom { get; set; } = null!;

    public virtual TodoListe IdTodoNavigation { get; set; } = null!;

    public virtual ICollection<Utilisateur> IdUtilisateurs { get; set; } = new List<Utilisateur>();
}

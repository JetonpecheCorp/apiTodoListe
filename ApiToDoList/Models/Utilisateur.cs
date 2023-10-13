using System;
using System.Collections.Generic;

namespace ApiToDoList.Models;

public partial class Utilisateur
{
    public int Id { get; set; }

    public int IdRole { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<TodoListe> IdTodoListes { get; set; } = new List<TodoListe>();

    public virtual ICollection<Todo> IdTodos { get; set; } = new List<Todo>();
}

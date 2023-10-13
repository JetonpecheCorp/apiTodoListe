using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiToDoList.Models;

public partial class TrelloContext : DbContext
{
    public TrelloContext()
    {
    }

    public TrelloContext(DbContextOptions<TrelloContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Todo> Todos { get; set; }

    public virtual DbSet<TodoListe> TodoListes { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07B6178561");

            entity.ToTable("Role");

            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Todo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Todo__3214EC07FC22287B");

            entity.ToTable("Todo");

            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTodoNavigation).WithMany(p => p.Todos)
                .HasForeignKey(d => d.IdTodo)
                .HasConstraintName("FK__Todo__Nom__2B3F6F97");

            entity.HasMany(d => d.IdUtilisateurs).WithMany(p => p.IdTodos)
                .UsingEntity<Dictionary<string, object>>(
                    "TodoUtilisateur",
                    r => r.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("IdUtilisateur")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TodoUtili__IdUti__32E0915F"),
                    l => l.HasOne<Todo>().WithMany()
                        .HasForeignKey("IdTodo")
                        .HasConstraintName("FK__TodoUtili__IdTod__31EC6D26"),
                    j =>
                    {
                        j.HasKey("IdTodo", "IdUtilisateur").HasName("PK__TodoUtil__E257A5A4696A59BF");
                        j.ToTable("TodoUtilisateur");
                    });
        });

        modelBuilder.Entity<TodoListe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TodoList__3214EC078B80A343");

            entity.ToTable("TodoListe");

            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasMany(d => d.IdUtilisateurs).WithMany(p => p.IdTodoListes)
                .UsingEntity<Dictionary<string, object>>(
                    "TodoListUtilisateur",
                    r => r.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("IdUtilisateur")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TodoListU__IdUti__2F10007B"),
                    l => l.HasOne<TodoListe>().WithMany()
                        .HasForeignKey("IdTodoListe")
                        .HasConstraintName("FK__TodoListU__IdTod__2E1BDC42"),
                    j =>
                    {
                        j.HasKey("IdTodoListe", "IdUtilisateur").HasName("PK__TodoList__9ADB7BC73B7B7147");
                        j.ToTable("TodoListUtilisateur");
                    });
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Utilisat__3214EC07C9EDE866");

            entity.ToTable("Utilisateur");

            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Prenom)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Utilisate__IdRol__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

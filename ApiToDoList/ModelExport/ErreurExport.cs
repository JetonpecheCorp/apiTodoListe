namespace ApiToDoList.ModelExport;

public sealed record ErreurExport
{
    public required string Message { get; init; } = null!;
    public required string NomPropriete { get; init; } = null!;
}

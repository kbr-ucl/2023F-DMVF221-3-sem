using System.ComponentModel.DataAnnotations;

namespace DisconnecedEnitities.Data;

public class ToDo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    [Timestamp] public byte[]? Version { get; set; }
}
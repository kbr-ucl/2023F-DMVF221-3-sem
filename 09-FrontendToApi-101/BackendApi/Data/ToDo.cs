using System.ComponentModel.DataAnnotations;

namespace BackendApi.Data;

public class Todo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    [Timestamp] public byte[]? Version { get; set; }
}
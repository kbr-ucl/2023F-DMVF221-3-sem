namespace BackendApi.Dto;

public class TodoDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
    public byte[] Version { get; set; }
}
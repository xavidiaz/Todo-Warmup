namespace TodoWarmup.Models;

public class TodoItem
{
    public required Guid Id { get; set; }
    public required string Text { get; set; }
    public bool IsDone { get; set; } = false;
}

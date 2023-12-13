namespace ToDoApp.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string? Name { get; set; } = string.Empty;
        public DateTime? Created { get; set; }
        public bool Completed { get; set; }
    }
}

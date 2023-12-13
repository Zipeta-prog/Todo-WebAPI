namespace ToDoApp.Models
{
    public class AddItemDTO
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}

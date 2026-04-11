using Domain.Enums;

namespace Domain.EntityModels
{
    public class ToDoTask : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ToDoTaskStatus ToDoTaskStatus { get; set; } = ToDoTaskStatus.Pending;
        public DateTime? CompletedAt { get; set; }
    }
}

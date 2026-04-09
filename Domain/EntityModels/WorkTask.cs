using Domain.Enums;

namespace Domain.EntityModels
{
    public class WorkTask : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public DateTime? CompletedAt { get; set; }
    }
}

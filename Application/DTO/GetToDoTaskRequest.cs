using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class GetToDoTaskRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ToDoTaskStatus? ToDoTaskStatus { get; set; }
        public DateTime? CompletedAtFrom { get; set; }
        public DateTime? CompletedAtTo { get; set; }
    }
}

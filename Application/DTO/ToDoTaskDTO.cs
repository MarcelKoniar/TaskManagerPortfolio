using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Application.DTO
{
    public class ToDoTaskDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ToDoTaskStatus ToDoTaskStatus { get; set; } = ToDoTaskStatus.Pending;
        public DateTime? CompletedAt { get; set; }
    }
}

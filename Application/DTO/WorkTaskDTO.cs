using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Application.DTO
{
    public class WorkTaskDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public DateTime? CompletedAt { get; set; }
    }
}

using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class GetWorkTaskRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Status? Status { get; set; }
        public DateTime? CompletedAtFrom { get; set; }
        public DateTime? CompletedAtTo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using TaskManager.Domain.Common;

namespace TaskManager.Domain.Entities
{
    public class Task : AuditableEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
    }

    public enum Status
    {
        New,
        Active,
        Resolved,
        Closed
    }
}

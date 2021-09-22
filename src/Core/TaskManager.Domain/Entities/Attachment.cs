using System;

namespace TaskManager.Domain.Entities
{
    public class Attachment
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public Guid TaskId { get; set; }

        public Task Task { get; set; }
    }
}

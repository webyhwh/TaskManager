using AutoMapper;
using System;

namespace TaskManager.Application.Tasks.Queries.GetTasks
{
    public class AttachmentDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }

    public class AttachmentDtoProfile : Profile
    {
        public AttachmentDtoProfile()
        {
            CreateMap<Domain.Entities.Attachment, AttachmentDto>();
        }
    }
}

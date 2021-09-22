using AutoMapper;
using System;
using System.Collections.Generic;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Tasks.Queries.GetTasks
{
    /// <summary>
    /// Задача
    /// </summary>
    public class TaskDto
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Вложенные файлы
        /// </summary>
        public IEnumerable<AttachmentDto> Attachments { get; set; }
    }

    public class TaskDtoProfile : Profile
    {
        public TaskDtoProfile()
        {
            CreateMap<Task, TaskDto>();
        }
    }
}

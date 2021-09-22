using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.Tasks.Commands.CreateTask
{
    /// <summary>
    /// Ответ запроса на создание задачи
    /// </summary>
    public class CreateTaskResponse
    {
        public CreateTaskResponse(Guid taskId)
        {
            TaskId = taskId;
        }

        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        [Required]
        public Guid TaskId { get; set; }
    }
}

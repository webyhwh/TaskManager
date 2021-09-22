using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.Common
{
    /// <summary>
    /// Ответ об ошибке
    /// </summary>
    [Serializable]
    public class ErrorResponse
    {
        public ErrorResponse(int code, string message, string cause = "")
        {
            Code = code;
            Message = message;
            Cause = cause;
        }

        /// <summary>
        /// Код ошибки
        /// </summary>
        [Required]
        public int Code { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Детальная информация об ошибке
        /// </summary>
        [Required]
        public string Cause { get; set; }
    }
}

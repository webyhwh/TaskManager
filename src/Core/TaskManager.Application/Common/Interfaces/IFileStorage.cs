using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace TaskManager.Application.Common.Interfaces
{
    public interface IFileStorage
    {
        Task<string> Store(IFormFile file, Guid taskId);

        void Delete(string filePath);
    }
}

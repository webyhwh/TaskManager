using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using TaskManager.Application.Common;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Infrastructure
{
    public class LocalDiskFileStorage : IFileStorage
    {
        private IConfiguration _configuration;

        public LocalDiskFileStorage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Delete(string filePath)
        {
            File.Delete(filePath);
        }

        public async Task<string> Store(IFormFile file, Guid taskId)
        {
            string attachments = Path.Combine(_configuration.GetValue<string>(Consts.ConfigurationNames.LocalStorageDirectory), taskId.ToString());
            Directory.CreateDirectory(attachments);
            if (file.Length > 0)
            {
                string filePath = Path.Combine(attachments, Path.GetRandomFileName());
                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                }

                return filePath;
            }

            return String.Empty;
        }
    }
}

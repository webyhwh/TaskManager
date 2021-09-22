using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Application.Common.Interfaces
{
    public interface IAttachmentService
    {
        Task<Guid> Add(IFormFile file, Guid taskId, CancellationToken cancellationToken);

        Task Delete(Guid attachmentId, CancellationToken cancellationToken);
    }
}

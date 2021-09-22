using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Common.Interfaces
{
    public interface ITaskManagerDbContext
    {
        DbSet<Domain.Entities.Task> Tasks { get; set; }

        DbSet<Attachment> Attachments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

using JC.Core.Models;
using JC.Core.Models.Auditing;
using Microsoft.EntityFrameworkCore;

namespace JC.Core.Data;

public interface IDataDbContext
{
    DbSet<ReportedIssue> ReportedIssues { get; }
    DbSet<AuditEntry> AuditEntries { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
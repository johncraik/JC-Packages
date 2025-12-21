using System.Text.Json;
using JC.Core.Data;
using JC.Core.Models;
using JC.Core.Models.Auditing;

namespace JC.Core.Services;

public class AuditService
{
    private readonly IDataDbContext _context;
    private readonly IUserInfo _userInfo;

    public AuditService(IDataDbContext context, IUserInfo userInfo)
    {
        _context = context;
        _userInfo = userInfo;
    }

    public async Task LogAsync(AuditAction action, string tableName, object? data = null)
    {
        var entry = new AuditEntry
        {
            Action = action,
            TableName = tableName,
            UserId = _userInfo.UserId,
            UserName = _userInfo.DisplayName ?? _userInfo.Username,
            ActionData = data != null ? JsonSerializer.Serialize(data) : null,
            AuditDate = DateTime.UtcNow
        };

        await _context.AuditEntries.AddAsync(entry);
        await _context.SaveChangesAsync();
    }

    public async Task LogCreateAsync(string tableName, object? data = null)
        => await LogAsync(AuditAction.Create, tableName, data);

    public async Task LogUpdateAsync(string tableName, object? data = null)
        => await LogAsync(AuditAction.Update, tableName, data);

    public async Task LogDeleteAsync(string tableName, object? data = null)
        => await LogAsync(AuditAction.Delete, tableName, data);

    public async Task LogRestoreAsync(string tableName, object? data = null)
        => await LogAsync(AuditAction.Restore, tableName, data);
}
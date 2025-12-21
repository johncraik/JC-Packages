using JC.Core.Data;
using JC.Core.Helpers;
using JC.Core.Models;
using Microsoft.Extensions.Configuration;

namespace JC.Core.Services;

public class BugReportService
{
    private readonly IDataDbContext _context;
    private readonly GitHelper _gitHelper;
    private readonly string _owner;
    private readonly string _repo;

    public BugReportService(IConfiguration config,
        IDataDbContext context,
        GitHelper gitHelper)
    {
        _context = context;
        _gitHelper = gitHelper;
        _owner = config["Github:Owner"] ?? throw new ArgumentNullException(nameof(config));
        _repo = config["Github:Repo"] ?? throw new ArgumentNullException(nameof(config));
    }
    
    public async Task<ReportedIssue> RecordIssue(string description, IssueType issueType, string? creatorId = null, string? creatorName = null)
    {
        var ri = new ReportedIssue
        {
            Description = description,
            Type = issueType,
            Created = DateTime.Now,
            ReportSent = true,
            UserId = creatorId,
            UserDisplay = creatorName
        };

        var issueNumber = await _gitHelper.RecordIssue(_owner, _repo, "New " + issueType, description);
        ri.ReportSent = true;
        ri.ExternalId = issueNumber;

        await _context.ReportedIssues.AddAsync(ri);
        await _context.SaveChangesAsync();
        return ri;
    }
}
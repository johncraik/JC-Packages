using JC.Core.Enums;
using JC.Core.Models.Auditing;

namespace JC.Core.Extensions;

public static class QueryExtensions
{
    public static IQueryable<T> FilterDeleted<T>(this IQueryable<T> query, DeletedQueryType deletedQueryType)
        where T : AuditModel
        => query.Where(x => deletedQueryType == DeletedQueryType.All 
                            || x.IsDeleted == (deletedQueryType == DeletedQueryType.OnlyDeleted));
}
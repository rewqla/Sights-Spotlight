using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StoreDAL.Entities;

namespace StoreDAL.Data;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly List<AuditEntry> _auditEntries;

    public AuditInterceptor(List<AuditEntry> auditEntries)
    {
        _auditEntries = auditEntries;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
            
        var startTime=DateTime.UtcNow;
        var auditEntries = eventData.Context.ChangeTracker
            .Entries()
            .Where(x => x.Entity is not AuditEntry &&
                        x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .Select(x => new AuditEntry
            {
                Id = Guid.NewGuid(),
                StartTimeUTC = startTime,
                Metadata = x.DebugView.LongView,
                TrailType = GetTrailType(x.State),
                EntityName = x.Entity.GetType().Name,
            }).ToList();
        
        if (auditEntries.Count==0)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
            
        _auditEntries.AddRange(auditEntries);
        
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is null)
        {
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }
        
        var endTime = DateTime.UtcNow;

        foreach (var auditEntry in _auditEntries)
        {
            auditEntry.EndTimeUTC = endTime;
            auditEntry.Succeed = true;
        }

        if (_auditEntries.Count > 0)
        {
            eventData.Context.Set<AuditEntry>().AddRange(_auditEntries);
            _auditEntries.Clear();
            await eventData.Context.SaveChangesAsync(cancellationToken);
        }
        
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public override async void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        if (eventData.Context is null)
        {
            return;
        }
        
        var endTime = DateTime.UtcNow;

        foreach (var auditEntry in _auditEntries)
        {
            auditEntry.EndTimeUTC = endTime;
            auditEntry.Succeed = false;
            auditEntry.ErrorMessage = eventData.Exception.Message;
        }

        if (_auditEntries.Count > 0)
        {
            eventData.Context.Set<AuditEntry>().AddRange(_auditEntries);
            _auditEntries.Clear();
            await eventData.Context.SaveChangesAsync();
        }
    }
    private string GetTrailType(EntityState state)
    {
        return state switch
        {
            EntityState.Added => "Create",
            EntityState.Modified => "Update",
            EntityState.Deleted => "Delete",
            _ => "Unknown"
        };
    }
}

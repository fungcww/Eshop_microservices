
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;
            foreach(var entry in context.ChangeTracker.Entries<IEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = "mehmet";
                    entry.Entity.CreateAt = DateTime.UtcNow;
                }
                if (entry.State == EntityState.Added 
                    || entry.State == EntityState.Modified
                    || entry.HasChangedOwnedEntities())
                {
                    entry.Entity.LastModifiedBy = "mehmet";
                    entry.Entity.LastModified = DateTime.UtcNow;
                }
            }
        }
    }
}

public static class Extentions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));

    //the user can use this extension method to check if the entry has changed owned entities
    //owned entities are entities that are owned by another entity
    //for example, an order can have multiple order items, and each order item can have multiple order item details
    //so the order item is owned by the order
}

using Core.Domain.Entities;
using Persistance.SqlData.Models;
using Microsoft.EntityFrameworkCore;
using Persistance.SqlData.Extensions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Utilities.Extensions;
using System.Reflection;

namespace Persistance.SqlData.Context
{

    public class CommandBaseContext : DbContext
    {
        private IDbContextTransaction _transaction;


        private bool IsUseAuditLog { get; set; } = true;
        public DbSet<Audit> AuditLogs { get; set; }
        public CommandBaseContext(DbContextOptions dbContext) : base(dbContext)
        {

        }


        public void EnableAuditLog()
        {
            IsUseAuditLog = true;
        }


        public void DisableAuditLog()
        {
            IsUseAuditLog = false;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(CommandBaseContext).Assembly);   
            builder.AddIsDeletedQueryFilter();
        }

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
            {
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");
            }
            _transaction.Commit();
        }

        public void RollbackTransaction()
        {
            if (_transaction == null)
            {
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");
            }
            _transaction.Rollback();
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            beforeSaveTriggers();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(
         bool acceptAllChangesOnSuccess,
         CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            beforeSaveTriggers();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }
        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }
        private void beforeSaveTriggers()
        {
            //var userService = this.GetService<IUserInfoService>();
            _cleanString();
            SoftDeleted();
            AuditSet(null);
        }

        private void AuditSet(Guid? uid)
        {
            if (IsUseAuditLog)
            {
                var audits = ChangeTracker.AuditSet(uid);
                AuditLogs.AddRange(audits);
            }
        }

        private void SoftDeleted()
        {
            var markedAsDeleted = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);

            foreach (var item in markedAsDeleted)
            {
                if (item.Entity is ISDeleted entity)
                {
                    // Set the entity to unchanged (if we mark the whole entity as Modified, every field gets sent to Db as an update)
                    item.State = EntityState.Unchanged;
                    // Only update the IsDeleted flag - only this will get sent to the Db
                    entity.IsDeleted = true;
                }
            }
        }


    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace InsuranceApi.Helpers
{
    public static class EntityFrameworkHelper
    {
        public static DbContext? GetContext<TEntity>(this DbSet<TEntity> dbSet)
        where TEntity : class
        {
            var internalSet = dbSet?
                .GetType()?.GetField("_internalSet", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(dbSet);
            var internalContext = internalSet?
                .GetType()?.BaseType?.GetField("_internalContext", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(internalSet);
            return internalContext?
                .GetType()?.GetProperty("Owner", BindingFlags.Instance | BindingFlags.Public)?.GetValue(internalContext, null) as DbContext;
        }

        public static string GetTableName<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class
        {
            var dbContext = dbSet.GetContext();

            var model = dbContext?.Model;
            var entityTypes = model?.GetEntityTypes();
            var entityType = entityTypes?.First(t => t.ClrType == typeof(TEntity));
            var tableNameAnnotation = entityType?.GetAnnotation("Relational:TableName");
            var tableName = tableNameAnnotation?.Value?.ToString();
            return tableName!;
        }

        public static void Truncate<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class
        {
            var tableName = dbSet.GetTableName();
            dbSet.TrancateByName(tableName);
        }

        public static void TrancateByName<TEntity>(this DbSet<TEntity> dbSet, string tableName) where TEntity : class
        {
            string cmd = $"TRUNCATE TABLE {tableName}";
            var context = dbSet.GetService<ICurrentDbContext>().Context;
            context.Database.ExecuteSqlRaw(cmd);
        }

        public static string Delete<T>(this DbSet<T> dbSet) where T : class
        {
            string cmd = $"DELETE FROM {dbSet.GetTableName()}";
            var context = dbSet.GetService<ICurrentDbContext>().Context;
            context.Database.ExecuteSqlRaw(cmd);
            return cmd;
        }

        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            if (dbSet.Any())
            {
                dbSet.RemoveRange(dbSet.ToList());
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace MiniORM
{
 
    public abstract class DbContext
    {
        
        private readonly string connectionString;

        
        protected DbContext(string connectionString)
        {
            this.connectionString = connectionString;

        
            this.MapDbSets();
        }

       
        private void MapDbSets()
        {
            var dbSetProperties = this.GetType()
         .GetProperties()
         .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
         .ToArray();

            foreach (var property in dbSetProperties)
            {
                
                Type entityType = property.PropertyType.GetGenericArguments()[0];

               
                Type dbSetGenericType = typeof(DbSet<>).MakeGenericType(entityType);

               
                Type listGenericType = typeof(List<>).MakeGenericType(entityType);
                object emptyList = Activator.CreateInstance(listGenericType);

                
                object dbSetInstance = Activator.CreateInstance(dbSetGenericType, emptyList);

                
                property.SetValue(this, dbSetInstance);
            }
        }


        public int SaveChanges()
        {
            int rowsAffected = 0; 

           
            var dbSetProperties = this.GetType()
                .GetProperties()
                .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToArray();

            foreach (var property in dbSetProperties)
            {
                
                var dbSetInstance = property.GetValue(this);
                if (dbSetInstance == null) continue;

                
                var changeTrackerProperty = dbSetInstance.GetType().GetProperty("ChangeTracker", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                var changeTracker = changeTrackerProperty.GetValue(dbSetInstance);

              
                var addedEntities = (IEnumerable<object>)changeTracker.GetType().GetProperty("Added").GetValue(changeTracker);
                var removedEntities = (IEnumerable<object>)changeTracker.GetType().GetProperty("Removed").GetValue(changeTracker);

              
                foreach (var entity in addedEntities)
                {
                    rowsAffected++;
                    string tableName = entity.GetType().Name; // Името на класа е името на таблицата ни
                    Console.WriteLine($"[SQL GENERATED]: INSERT INTO {tableName} VALUES (New Entity Detected in C#!)");
                }

               
                foreach (var entity in removedEntities)
                {
                    rowsAffected++;
                    string tableName = entity.GetType().Name;
                    Console.WriteLine($"[SQL GENERATED]: DELETE FROM {tableName} WHERE Entity was removed from DbSet!");
                }
            }

            Console.WriteLine($"--- SaveChanges completed successfully. {rowsAffected} row(s) updated. ---");
            return rowsAffected;
        }
    }
}

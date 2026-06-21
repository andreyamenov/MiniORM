using System;
using System.Collections;
using System.Collections.Generic;

namespace MiniORM
{
    public class DbSet<TEntity> : ICollection<TEntity>
        where TEntity : class, new()
    {
        internal IList<TEntity> Entities { get; set; }

       
        internal ChangeTracker<TEntity> ChangeTracker { get; set; }

        internal DbSet(IEnumerable<TEntity> entities)
        {
            this.Entities = new List<TEntity>(entities);
          
            this.ChangeTracker = new ChangeTracker<TEntity>(entities);
        }

        public int Count => this.Entities.Count;
        public bool IsReadOnly => false;

        public bool Contains(TEntity item)
        {
            return this.Entities.Contains(item);
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            this.Entities.CopyTo(array, arrayIndex);
        }

        public void Add(TEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            }

            this.Entities.Add(item);
            
            this.ChangeTracker.Add(item);
        }

        public bool Remove(TEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            }

            bool hasRemoved = this.Entities.Remove(item);

           
            if (hasRemoved)
            {
                this.ChangeTracker.Remove(item);
            }

            return hasRemoved;
        }

        public void Clear()
        {
            
            while (this.Entities.Count > 0)
            {
                this.Remove(this.Entities[0]);
            }
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

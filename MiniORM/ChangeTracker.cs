using System;
using System.Collections.Generic;

namespace MiniORM
{
    public class ChangeTracker<T> where T : class, new()
    {
        
        private readonly List<T> allEntities;
        private readonly List<T> added;
        private readonly List<T> removed;

        public IReadOnlyCollection<T> AllEntities => this.allEntities.AsReadOnly();
        public IReadOnlyCollection<T> Added => this.added.AsReadOnly();
        public IReadOnlyCollection<T> Removed => this.removed.AsReadOnly();

        public ChangeTracker(IEnumerable<T> entities)
        {
            this.added = new List<T>();
            this.removed = new List<T>();

            this.allEntities = new List<T>(entities);
        }
        public void Add(T item)
        {
            this.added.Add(item);
        }

        public void Remove(T item)
        {
            this.removed.Add(item);
        }
    }
}

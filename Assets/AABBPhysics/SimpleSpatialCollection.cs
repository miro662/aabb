using System.Collections.Generic;

namespace AABB
{
    public class SimpleSpatialCollection<T>: ISpatialCollection<T>
    {
        private IList<T> items;
        
        public SimpleSpatialCollection()
        {
            items = new List<T>();
        }
        
        public void Add(T item, Bounds position)
        {
            items.Add(item);
        }

        public void Move(T item, Bounds newPosition)
        {
        }

        public void Remove(T item)
        {
            items.Remove(item);
        }

        public IEnumerable<T> Get(Bounds area)
        {
            return items;
        }
    }
}
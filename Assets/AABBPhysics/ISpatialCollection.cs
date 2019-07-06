using System.Collections.Generic;

namespace AABB
{
    public interface ISpatialCollection<T>
    {
        void Add(T item, Bounds position);
        void Move(T item, Bounds newPosition);
        void Remove(T item);
        IEnumerable<T> Get(Bounds area);
    }
}
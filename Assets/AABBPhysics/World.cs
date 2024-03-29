using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AABB
{
    public class World
    {
        private ISpatialCollection<Collider> _colliders;

        public World(ISpatialCollection<Collider> collidersCollection)
        {
            _colliders = collidersCollection;
        }


        public void AddCollider(Collider collider)
        {
            _colliders.Add(collider, collider.bounds);
        }
        
        public void MoveCollider(Collider collider)
        {
            _colliders.Move(collider, collider.bounds);
        }

        public void RemoveCollider(Collider collider)
        {
            _colliders.Remove(collider);
        }

        public IEnumerable<Collision> Cast(Bounds bounds)
        {
            foreach (Collider rigidbody in _colliders.Get(bounds))
            {
                var intersection = bounds.intersection(rigidbody.bounds);
                if (intersection.HasValue)
                {
                    yield return new Collision(rigidbody, intersection.Value);
                }
            }
        }
    }
}
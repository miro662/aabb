using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AABB
{
    //TODO: make better World, using spatial algorithm (QuadTree)
    public class World: MonoBehaviour
    {
        private ISpatialCollection<Collider> _colliders;

        private void Awake()
        {
            // TODO: ability to use other spatial colletions
            _colliders = new SimpleSpatialCollection<Collider>();
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
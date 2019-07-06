using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

namespace AABB
{
    public class Collider: MonoBehaviour
    {
        [SerializeField] World world;
        [SerializeField] Vector2 size = Vector2.one;

        private const float inset = 0.01f;
        
        public Bounds bounds => new Bounds(transform.position, size);
        public Bounds insetBounds => new Bounds(transform.position, size - inset * Vector2.one);

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(bounds.BottomLeft, bounds.BottomRight);
            Gizmos.DrawLine(bounds.TopLeft, bounds.TopRight);
            Gizmos.DrawLine(bounds.BottomLeft, bounds.TopLeft);
            Gizmos.DrawLine(bounds.BottomRight, bounds.TopRight);
        }

        public IEnumerable<Collision> Cast() => world.Cast(bounds);

        public IEnumerable<Collision> CastWithInset() => world.Cast(insetBounds);

        public IEnumerable<Collision> CastMoved(Vector2 translation) =>
            world.Cast(new Bounds(bounds.Center + translation, bounds.Size));
        
        private void OnEnable()
        {
            world.AddCollider(this);
        }

        private void OnDisable()
        {
            world.RemoveCollider(this);
        }

        // usage necessary because World might use spatial algorithm
        // moving object in different way causes undefined behaviour
        public void Translate(Vector2 translation, Space relativeTo)
        {
            transform.Translate(translation, relativeTo);
            world.AddCollider(this);
        }
    }
}
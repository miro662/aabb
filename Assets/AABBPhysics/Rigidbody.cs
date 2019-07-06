using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AABB
{
    [RequireComponent(typeof(Collider))]
    public class Rigidbody: MonoBehaviour
    {
        private Collider _collider;

        [Serializable]
        public struct CollisionMasks
        {
            [SerializeField] public LayerMask solidMask;
            [SerializeField] public LayerMask leftMask;
            [SerializeField] public LayerMask rightMask;
            [SerializeField] public LayerMask upMask;
            [SerializeField] public LayerMask downMask;

            public LayerMask GetXMask(float direction) => solidMask | (direction > 0 ? rightMask : leftMask);
            public LayerMask GetYMask(float direction) => solidMask | (direction > 0 ? upMask : downMask);
        }

        [SerializeField] private CollisionMasks masks;
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public Vector2 Move(Vector2 translation)
        {
            return new Vector2(
            MoveDimension(translation.x, Vector2.right, masks.GetXMask(translation.x), v => v.x),
            MoveDimension(translation.y, Vector2.up, masks.GetYMask(translation.y), v => v.y)
            );
        }
        
        private float MoveDimension(float translation, Vector2 direction, LayerMask mask, Func<Vector2, float> dimensionGetter)
        {
            var currentColliders = CurrentColliders;
            
            var newBoundsCollisions = _collider.CastMoved(translation * direction);

            var newCollisions = newBoundsCollisions
                .Where(c => ((1 << c.Collider.gameObject.layer) & mask) != 0) // filter out colliders from wrong masks
                .Where(c => c.Collider != _collider)
                .Where(c => !currentColliders.Contains(c.Collider));

            var biggestBound = 0f;
            if (newCollisions.Count() > 0)
            {
                biggestBound = newCollisions // filter out objects not in mask
                    .Select(c => dimensionGetter(c.bounds.Size))
                    .Max();
            }

            translation -= biggestBound;
            _collider.Translate(translation * direction, Space.Self);
            return translation;
        }
        
        private IEnumerable<Collider> CurrentColliders
        {
            get
            {
                var currentCollisions = _collider.CastWithInset();
                var currentColliders = currentCollisions.Select(c => c.Collider);
                return currentColliders;
            }
        }
    }
}
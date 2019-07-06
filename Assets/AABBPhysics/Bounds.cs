using System;
using UnityEditor.UIElements;
using UnityEngine;

namespace AABB
{
    public struct Bounds
    {
        public Vector2 Center;
        public Vector2 Size;

        public Vector2 TopRight => Center + Size / 2;
        public Vector2 BottomLeft => Center - Size / 2;
        public Vector2 TopLeft => new Vector2(BottomLeft.x, TopRight.y);
        public Vector2 BottomRight => new Vector2(TopRight.x, BottomLeft.y);
        
        private Range xRange => new Range(BottomLeft.x, TopRight.x);
        private Range yRange => new Range(BottomLeft.y, TopRight.y);

        public Bounds(Vector2 center, Vector2 size)
        {
            Center = center;
            Size = size;
        }

        public static Bounds fromCorners(Vector2 topRight, Vector2 bottomLeft)
        {
            Vector2 center = (topRight + bottomLeft) / 2;
            Vector2 size = topRight - bottomLeft;
            return new Bounds(center, size);
        }

        private struct Range
        {
            public float a;
            public float b;

            public Range(float a, float b)
            {
                this.a = a;
                this.b = b;
            }

            public Range? intersection(Range other)
            {
                Range intersection;
                intersection.a = Mathf.Max(this.a, other.a);
                intersection.b = Mathf.Min(this.b, other.b);
                if (intersection.a <= intersection.b)
                {
                    return intersection;
                }
                else
                {
                    return null;
                }
            }
        }

        public Bounds? intersection(Bounds other)
        {
            var intersectionX = xRange.intersection(other.xRange);
            var intersectionY = yRange.intersection(other.yRange);
            if (intersectionX.HasValue && intersectionY.HasValue)
            {
                return fromCorners(
                    bottomLeft: new Vector2(intersectionX.Value.a, intersectionY.Value.a), 
                    topRight: new Vector2(intersectionX.Value.b, intersectionY.Value.b)
                );
            }
            else
            {
                return null;
            }
        }

        public bool Overlaps(Bounds other)
        {
            return this.intersection(other).HasValue;
        }

        public override string ToString()
        {
            return "AABB Bounds, center: " + Center + ", size: " + Size;
        }
    }
}
namespace AABB
{
    public struct Collision
    {
        public Collider Collider;
        public Bounds bounds;

        public Collision(Collider collider, Bounds bounds)
        {
            this.Collider = collider;
            this.bounds = bounds;
        }
    }
}
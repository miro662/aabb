using System;
using UnityEngine;

namespace AABB
{
    public class Mover: MonoBehaviour
    {
        public Vector2 velocity;
        private void FixedUpdate()
        {
            GetComponent<Rigidbody>().Move(velocity * Time.fixedDeltaTime);
        }
    }
}
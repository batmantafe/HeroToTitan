using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperMarioGalaxy
{
    public class GravityNormal : GravityBody
    {
        public float force = -10f;
        public float rayDistance = 5f;
        public LayerMask hitLayers;
        public bool isGrounded = false;
        public RaycastHit groundHit;

        private Ray groundRay;
        private Vector3 gravity;

 

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * rayDistance);
        }

        void FixedUpdate()
        {
            groundRay = new Ray(transform.position, -transform.up);
            RaycastHit[] hits = Physics.RaycastAll(groundRay, rayDistance, hitLayers);

            foreach (RaycastHit hit in hits)
            {
                // Is the hit not ourself?
                if (hit.collider.name != name)
                {
                    // return new normal
                    groundHit = hit;
                    isGrounded = true;
                    return;
                }
            }

            // All else, set isGrounded to false
            isGrounded = false;
        }

        public override Vector3 Gravity
        {
            get
            {
                if (isGrounded)
                {
                    return groundHit.normal * force;
                }

                return base.Gravity;
            }
        }

        public Vector3 HitPoint
        {
            get
            {
                return groundHit.point;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperMarioGalaxy
{
    [RequireComponent(typeof(Rigidbody))]

    public class GravityBody : MonoBehaviour
    {
        public bool useGravity = true;

        private List<GravityAttractor> attractors = new List<GravityAttractor>();

        private Rigidbody rigid;
        private Vector3 normal;

        public virtual Vector3 Gravity
        {
            get
            {
                // is useGravity false?
                if (!useGravity)
                {
                    // Return no gravity
                    return Vector3.zero;
                }
                
                // If there are no attractions
                if (attractors.Count == 0)
                {
                    // Return default gravity
                    return Physics.gravity;
                }

                // Reset gravity before calculating
                Vector3 gravity = Vector3.zero;

                // Loop through each gravity attractor
                foreach (GravityAttractor a in attractors)
                {
                    // Append gravity
                    gravity += a.GetGravity(transform.position);

                }

                // Return the gravity
                return gravity;
            }
        }

        public Vector3 Velocity
        {
            get
            {
                return rigid.velocity;
            }

            set
            {
                rigid.velocity = value;
            }
        }

        public void AddForce(Vector3 velocity, ForceMode forceMode = ForceMode.Acceleration)
        {
            rigid.AddForce(velocity, forceMode);
        }

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
            rigid.useGravity = false;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 gravity = Gravity;
            rigid.AddForce(Gravity);

            normal = Vector3.Lerp(normal, gravity, 10 * Time.deltaTime);

            // Rotate to surface normal
            transform.up = -normal;
        }

        void OnTriggerEnter(Collider other)
        {
            // Try getting gravity attractor component
            GravityAttractor a = other.GetComponent<GravityAttractor>();

            if (a != null)
            {
                // Add attractor to the list
                attractors.Add(a);
            }
        }

        void OnTriggerExit(Collider other)
        {
            // Try getting gravity attractor component
            GravityAttractor a = other.GetComponent<GravityAttractor>();

            if (a != null)
            {
                // Remove attractor to the list
                attractors.Remove(a);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GGL;

namespace SuperMarioGalaxy
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        public float speed = 20f;
        public float maxVelocity = 10f;

        private Rigidbody rigid;
        private GravityNormal body;

        private Vector3 force;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + force);
        }

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
            body = GetComponent<GravityNormal>();
        }

        // Update is called once per frame
        public void Move(float inputH, float inputV)
        {
            Vector3 localForce = new Vector3(inputH, 0, inputV);
            force = Quaternion.LookRotation(Camera.main.transform.up, -body.Gravity) * localForce;

            GizmosGL.AddLine(transform.position, transform.position + force, 0.3f, 0.3f, Color.blue, Color.blue);

            rigid.AddForce(force * speed);

            // If velocity reaches higher than max velocity
            if (rigid.velocity.magnitude > maxVelocity)
            {
                // cap that shit bish
                rigid.velocity = rigid.velocity.normalized * maxVelocity;
            }
            
        }
    }
}

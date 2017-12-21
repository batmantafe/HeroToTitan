using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFox
{
    [RequireComponent(typeof(Laser))]

    public class HitDetection : MonoBehaviour
    {
        public GameObject hitPrefab;
        public GameObject impactPrefab;
        public string[] triggerTags = new string[] { "Player" };
        public string[] collisionTags = new string[] { "Ground" };

        private Laser laser;

        // Use this for initialization
        void Start()
        {
            laser = GetComponent<Laser>();
        }

        void Hit(Collider other)
        {
            // Instantiate hitPrefab and impactPrefab
            // Apply damage to object
            Health health = other.GetComponent<Health>();

            if (health != null)
            {
                health.DealDamage(laser.damage);
            }

            Destroy(gameObject);
        }

        // Update is called once per frame
        void OnTriggerEnter(Collider other)
        {
            for (int i = 0; i < triggerTags.Length; i++)
            {
                string triggerTag = triggerTags[i];

                // If we have collided with an object that has the collisionTag AND
                // The Laser's owner is not the thing we hit

                if (other.tag == triggerTag && laser.owner != other.transform)
                {
                    // Instantiate hitPrefab and impactPrefab
                    // Apply damage to object
                    Hit(other);
                }

            }
        }

        void OnCollisionEnter(Collision other)
        {
            for (int i = 0; i < collisionTags.Length; i++)
            {
                string collisionTag = collisionTags[i];

                // If we have collided with an object that has the collisionTag AND
                // The Laser's owner is not the thing we hit

                if (other.collider.tag == collisionTag && laser.owner != other.transform)
                {
                    // Instantiate hitPrefab and impactPrefab
                    // Apply damage to object
                    Hit(other.collider);
                }

            }
        }
    }
}

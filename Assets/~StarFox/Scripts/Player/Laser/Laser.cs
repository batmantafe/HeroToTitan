using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFox
{
    public class Laser : MonoBehaviour
    {
        public GameObject firePrefab; // Sound
        public Vector3 direction;
        public float damage = 25f;
        public float speed = 300f;
        public float lifeTime = 5f;
        public float shootRate = 0.175f;

        [HideInInspector]
        public Transform owner;

        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        // Update is called once per frame
        void Update()
        {
            if (direction.magnitude != 0)
            {
                // Only call 'LookRotation' if Vector is not Zero
                transform.rotation = Quaternion.LookRotation(direction);
            }

            transform.position += direction * speed * Time.deltaTime;
        }
    }
}

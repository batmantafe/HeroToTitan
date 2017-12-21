using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFox
{
    public class ArwingShoot : MonoBehaviour
    {
        public GameObject muzzleFlashPrefab;

        [Header("Laser")]
        public Laser[] laserPrefab;

        [Header("Pulse")]
        public GameObject lockOnUI;
        public GameObject chargeEffectPrefab;
        public GameObject fireEffectPrefab;
        public float chargeDelay = 1f;

        private int currentLaser = 0;
        private float shootTimer = 0f;
        
        // Update is called once per frame
        void Update()
        {
            shootTimer += Time.deltaTime;
        }

        GameObject FireLaser()
        {
            // Instantiate the current laser selected
            GameObject clone = Instantiate(laserPrefab[currentLaser].gameObject);

            // Set position and rotation of bullet to Arwing
            clone.transform.position = transform.position;
            clone.transform.rotation = transform.rotation;

            // Set direction of laser
            Laser laser = clone.GetComponent<Laser>();
            laser.direction = transform.forward;

            laser.owner = transform; // Set Owner to Self

            return clone;
        }

        public GameObject Shoot()
        {
            Laser laserToShoot = laserPrefab[currentLaser];

            if (shootTimer >= laserToShoot.shootRate)
            {
                shootTimer = 0f;
                return FireLaser();
            }

            return null;
        }
    }
}

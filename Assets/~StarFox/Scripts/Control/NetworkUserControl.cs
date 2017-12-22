using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

namespace StarFox
{
    public class NetworkUserControl : NetworkBehaviour
    {
        public ArwingController controller;
        public ArwingShoot weapon;

        [Header("References")]
        public Camera cam;
        public AudioListener audioListener;
        public GameObject canvas;

        void Start()
        {
            // If we are not the local player
            if (!isLocalPlayer)
            {
                // Disable all references
                cam.enabled = false;
                audioListener.enabled = false;
                canvas.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Check if player is local player
            if (isLocalPlayer)
            {
                float inputH = Input.GetAxis("Horizontal");
                float inputV = Input.GetAxis("Vertical");
                controller.Move(inputH, inputV);

                // Check if local user pressed mouse button
                if(Input.GetMouseButton(0))
                {
                    // Weapon can shoot?
                    if (weapon.canShoot)
                    {
                        Cmd_Shoot();
                    }
                }
            }
        }

        [Command]
        void Cmd_Shoot()
        {
            // Spawn the bullet on the server
            GameObject laser = weapon.Shoot();

            // Check if laser was actually created
            if (laser != null)
            {
                // Spawn on network
                NetworkServer.Spawn(laser);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFox
{
    public class LocalUserControl : MonoBehaviour
    {
        public ArwingController controller;
        
        // Update is called once per frame
        void Update()
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");

            // Move the controller
            controller.Move(inputH, inputV);

            // Shoot Laser


            // Shoot Pulse

        }
    }
}

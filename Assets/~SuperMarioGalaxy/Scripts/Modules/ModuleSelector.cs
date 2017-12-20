using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperMarioGalaxy
{
    [RequireComponent(typeof(CameraFollowSphere))]

    public class ModuleSelector : MonoBehaviour
    {
        public float rayDistance = 100f;

        private Ray camRay;
        private CameraFollowSphere cam;

        private Module selectedModule;

        void OnDrawGizmos()
        {
            Gizmos.DrawLine(camRay.origin, camRay.origin + camRay.direction * rayDistance);
        }

        void Start()
        {
            cam = GetComponent<CameraFollowSphere>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits = Physics.RaycastAll(camRay, rayDistance);

            // Loop through all the hits
            foreach (RaycastHit hit in hits)
            {
                // Check if we hit module
                Module module = hit.collider.GetComponent<Module>();

                if (module != null)
                {
                    // Set as selected module
                    selectedModule = module;
                }
            }

            // Is there currently a selected module?
            if (selectedModule != null)
            {
                // Check if mouse button is down
                if (Input.GetMouseButton(0))
                {
                    // Perform the action with module using camera's target (GravityBody)
                    cam.target.useGravity = false;
                    selectedModule.Action(cam.target);
                    selectedModule.Enter();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    cam.target.useGravity = true;
                    selectedModule.Exit();
                    selectedModule = null; // No more selected module
                }
            }
        }
    }
}

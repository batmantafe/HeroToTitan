using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFox
{
    public class ArwingController : MonoBehaviour
    {
        public enum Mode
        {
            Confined = 0,
            AllRange = 1
        }

        public Mode mode = Mode.Confined; // Different modes the Arwing flies at

        [Header("Camera")]
        public float cameraYSpeed = .5f; // Y speed of the camera
        public float cameraMoveSpeed = 20f; // Movement speed of the camera
        public float cameraDistance = 5f; // Arwing's distance away from camera

        [Header("Arwing")]
        public MoveTarget moveTarget; // Transform to move to
        public Transform aimTarget; // Transform to rotate to
        public Vector3 rotationInfluence = new Vector3(.3f, 1); // Arwing's rotation influence
        public float aimingSpeed = 20f;
        public float movementSpeed = 20f;
        public float rotationSpeed = 20f;

        private Camera parentCam;
        private float startDistance = 5f;
        private Vector3 up = Vector3.up;

        void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + up * 10f);
        }

        // Use this for initialization
        void Start()
        {
            // Get camera from parent GameObject
            parentCam = GetComponentInParent<Camera>();

            // Get distance from camera to Arwing
            startDistance = Vector3.Distance(parentCam.transform.position, transform.position);
        }

        // Arwing Functions
        void MoveToMoveTarget()
        {
            // Get move target position
            Vector3 movePos = moveTarget.transform.position;

            // Move the Arwing towards movePos
            transform.position = Vector3.MoveTowards(transform.position, movePos, movementSpeed * Time.deltaTime);

            // Update position
            Vector3 localPos = transform.localPosition;
            localPos.z = startDistance;
            transform.localPosition = localPos;
        }

        void MoveToAimTarget()
        {
            // Get MoveTarget's position
            Vector3 moveTargetPos = moveTarget.transform.localPosition;

            float zPos = moveTargetPos.z; // Save the Z location for later

            // Move the MoveTarget to Arwing
            moveTargetPos = Vector3.MoveTowards(moveTargetPos, transform.localPosition, movementSpeed * Time.deltaTime);
            
            // Restore the z position of MoveTarget
            moveTargetPos.z = zPos;

            // Apply modified values
            moveTarget.transform.localPosition = moveTargetPos;
        }

        void RotateToAimTarget(float inputH)
        {
            // Get direction to AimTarget from Arwing
            Vector3 direction = aimTarget.position - transform.position;

            // If there is horizontal input
            if(inputH != 0)
            {
                // set up to new rotational influence
                up = rotationInfluence;

                // Invert it depending on which key was
                // pressed horizontally
                up += parentCam.transform.right * inputH;
            }

            // Rotate to direction
            Quaternion rotation = Quaternion.LookRotation(direction.normalized, up);

            // ...smoothly
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, aimingSpeed * Time.deltaTime);
        }

        // Camera Functions
        void MoveForward()
        {
            Vector3 camPos = parentCam.transform.position;
            Vector3 camForward = parentCam.transform.forward;
            camPos += camForward * cameraMoveSpeed * Time.deltaTime;
            parentCam.transform.position = camPos;
        }

        void FollowArwing()
        {
            // Get camera's position and rotation
            Vector3 camPos = parentCam.transform.position;
            Quaternion camRot = parentCam.transform.rotation;

            // Get local position of Arwing and AimTarget
            Vector3 localPos = transform.localPosition;
            Vector3 localMoveTargetPos = moveTarget.transform.localPosition;

            // Get direction to aimTarget
            Vector3 direction = localMoveTargetPos - localPos;

            // Rotate camera to direction
            camRot *= Quaternion.AngleAxis(direction.x * rotationSpeed * Time.deltaTime, Vector3.up);

            // Move camera pos up or down depending on the Arwing's position
            camPos.y += direction.y * cameraYSpeed * Time.deltaTime;

            // Apply modified values
            parentCam.transform.position = camPos;
            parentCam.transform.rotation = camRot;
        }

        public void Move(float inputH, float inputV)
        {
            MoveForward(); // Move camera forward

            // Check which mode the Arwing is set to
            switch (mode)
            {
                case Mode.Confined:
                    break;
                case Mode.AllRange:
                    // Get camera to follow Arwing
                    FollowArwing();
                    break;
                default:
                    break;
            }

            // If there is no input
            if (inputH == 0 && inputV == 0)
            {
                MoveToAimTarget();
            }

            // Move the moveTarget with input
            moveTarget.Move(inputH, inputV);

            // Move the AimTarget to MoveTarget
            MoveToMoveTarget();

            // Rotate Arwing to MoveTarget
            RotateToAimTarget(inputH);

            Vector3 direction = moveTarget.transform.position - transform.position;
            moveTarget.transform.rotation = Quaternion.LookRotation(direction.normalized);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

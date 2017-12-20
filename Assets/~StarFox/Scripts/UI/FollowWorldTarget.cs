using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFox
{
    public class FollowWorldTarget : MonoBehaviour
    {
        public Transform target;
        public Camera cam;

        // Update is called once per frame
        void Update()
        {
            if (target != null && cam != null)
            {
                transform.position = cam.WorldToScreenPoint(target.position);
            }


        }
    }
}

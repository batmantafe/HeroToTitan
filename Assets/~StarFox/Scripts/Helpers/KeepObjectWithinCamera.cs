using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFox
{
    public class KeepObjectWithinCamera : MonoBehaviour
    {
        public Camera cam;

        public Vector2 min = new Vector2(0, 0);
        public Vector2 max = new Vector2(1, 1);
        
        // Last event, best for Cameras
        void LateUpdate()
        {
            Vector3 pos = cam.WorldToViewportPoint(transform.position);

            if (pos.x < min.x)
            {
                pos.x = min.x;
            }

            if (pos.x > max.x)
            {
                pos.x = max.x;
            }

            if (pos.y < min.y)
            {
                pos.y = min.y;
            }

            if (pos.y > max.y)
            {
                pos.y = max.y;
            }

            transform.position = cam.ViewportToWorldPoint(pos);
        }
    }
}

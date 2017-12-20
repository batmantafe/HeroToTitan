using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperMarioGalaxy
{
    public class GravityAttractor : MonoBehaviour
    {
        public float gravity = -10f;

        public Vector3 GetGravity(Vector3 bodyPosition)
        {
            Vector3 direction = bodyPosition - transform.position;
            return direction.normalized * gravity;
        }
    }
}

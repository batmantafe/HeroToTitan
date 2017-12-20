using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFox
{
    public class MoveTarget : MonoBehaviour
    {
        public float moveSpeed = 20f;


        public void Move(float inputH, float inputV)
        {
            Vector3 direction = new Vector3(inputH, inputV, 0);
            Vector3 force = direction * moveSpeed;
            transform.localPosition += force * Time.deltaTime;


        }
    }
}

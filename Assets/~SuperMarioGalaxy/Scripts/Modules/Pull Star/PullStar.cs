using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperMarioGalaxy
{
    public class PullStar : Module
    {
        public float speed = 10f; // Speed at which it pulls a gravitybody

        public override void Action(GravityBody body)
        {
            Vector3 direction = transform.position - body.transform.position;

            body.AddForce(direction * speed);

            base.Action(body);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperMarioGalaxy
{
    public class Module : MonoBehaviour
    {
        public delegate void EventCallBack();
        public delegate void ActionCallback(GravityBody body);

        public EventCallBack onEnter;
        public EventCallBack onStay;
        public EventCallBack onExit;

        public ActionCallback onAction;

        public virtual void Enter()
        {
            // Check for subscribed functions
            if (onEnter != null)
            {
                onEnter.Invoke();
            }
        }

        public virtual void Stay()
        {
            if (onStay != null)
            {
                onStay.Invoke();
            }
        }

        public virtual void Exit()
        {
            if (onExit != null)
            {
                onExit.Invoke();
            }
        }

        public virtual void Action(GravityBody body)
        {
            if (onAction != null)
            {
                onAction.Invoke(body);
            }
        }

    }
}

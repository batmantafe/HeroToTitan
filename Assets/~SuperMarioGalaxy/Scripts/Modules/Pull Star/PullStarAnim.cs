using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperMarioGalaxy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PullStar))]

    public class PullStarAnim : MonoBehaviour
    {
        private Animator anim;
        private PullStar pullStar;
        private bool isHovering = false;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
            pullStar = GetComponent<PullStar>();

            // Subscribe to delegates
            pullStar.onEnter += OnEnter;
            pullStar.onExit += OnExit;
        }

        void OnEnter()
        {
            isHovering = true;
        }

        void OnExit()
        {
            isHovering = false;
        }

        // Update is called once per frame
        void Update()
        {
            anim.SetBool("IsHovering", isHovering);
        }
    }
}

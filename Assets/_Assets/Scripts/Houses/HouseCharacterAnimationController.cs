using System;
using Pathfinding;
using UnityEngine;

namespace _Assets.Scripts.Houses
{
    public class HouseCharacterAnimationController : MonoBehaviour
    {
        [SerializeField] private AIPath aiPath;
        [SerializeField] private Animator animator;
        private static readonly int Running = Animator.StringToHash("Running");

        private void Update()
        {
           UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            if (aiPath.reachedDestination)
            {
                animator.SetBool(Running, false);
            }
            else
            {
                animator.SetBool(Running, true);
            }
        }
    }
}
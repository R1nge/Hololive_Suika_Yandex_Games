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
            if (aiPath.reachedDestination || (float.IsInfinity(aiPath.destination.x) ||
                                              float.IsInfinity(aiPath.destination.y) ||
                                              float.IsInfinity(aiPath.destination.z)))
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
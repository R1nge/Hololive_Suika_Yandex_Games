using System;
using Pathfinding;
using UnityEngine;

namespace _Assets.Scripts.Houses
{
    public class HouseCharacterRotationController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 spritePosition = transform.position;

                if (mousePosition.x < spritePosition.x)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
    }
}
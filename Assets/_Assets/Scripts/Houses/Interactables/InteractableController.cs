using System;
using System.Linq;
using Pathfinding;
using UnityEngine;

namespace _Assets.Scripts.Houses.Interactables
{
    public class InteractableController : MonoBehaviour
    {
        [SerializeField] private AIPath character;
        private IInteractable _target;
        private Transform _targetTransform;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit.collider != null)
                {
                    if (hit.transform.TryGetComponent(out IInteractable interactable))
                    {
                        _target = interactable;
                        if (hit.transform.TryGetComponent(out GridCellView cell))
                        {
                            _targetTransform = cell.transform;

                            if (Vector2.Distance(character.position, cell.transform.position) > 1.05f)
                            {
                                return;
                            }

                            _target.Interact();
                        }
                    }
                }
            }

            if (!float.IsPositiveInfinity(character.remainingDistance) && character.remainingDistance < 0.01f)
            {
                if (_target == null || _targetTransform == null) return;

                if (Vector2.Distance(character.position, _targetTransform.position) > 1.05f)
                {
                    return;
                }

                _target.Interact();
                _target = null;
                _targetTransform = null;
            }
        }
    }
}
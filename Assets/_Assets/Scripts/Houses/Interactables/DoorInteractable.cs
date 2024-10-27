using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Assets.Scripts.Houses.Interactables
{
    public class DoorInteractable : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            SceneManager.LoadScene("Main");
        }
    }
}
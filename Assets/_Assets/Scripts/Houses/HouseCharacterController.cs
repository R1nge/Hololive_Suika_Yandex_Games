using System;
using UnityEngine;

namespace _Assets.Scripts.Houses
{
    public class HouseCharacterController : MonoBehaviour
    {
       [SerializeField] private GridView gridView;

       private void Awake()
       {
           transform.parent = gridView.transform;
       }

       private void Update()
       {
           if (Input.GetMouseButtonDown(0))
           {
               var cell = gridView.GetCellFromMousePosition();
               if (cell == null) return;
               MoveTo(cell);
           }
       }
       
       private void MoveTo(GridCellView cell)
       {
           Debug.Log(cell.X + " " + cell.Y);
           transform.localPosition = new Vector3(cell.X, cell.Y, 0);
       }
    }
}
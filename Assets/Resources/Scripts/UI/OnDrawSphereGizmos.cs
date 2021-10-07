using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDrawSphereGizmos : MonoBehaviour
{
    [SerializeField]
    private Color color = Color.red;
    [SerializeField]
    private float radius = 0.5f;

   private void OnDrawGizmos() 
   {
       Gizmos.color = color;
       Gizmos.DrawWireSphere(transform.position, radius);
   }
}

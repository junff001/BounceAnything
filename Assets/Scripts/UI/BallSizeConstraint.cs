using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSizeConstraint : MonoBehaviour
{
    public Canvas ballSizeCanvas;
  
    void Update()
    {
        CanvasPosition();
    }

    private void CanvasPosition()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            if (hit.transform.CompareTag("Player"))
            {
                ballSizeCanvas.transform.position = hit.point;
            }
        }
    }
}

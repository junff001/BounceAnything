using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float rotatePower = 50;

    void Update()
    {
        ObjRotate();
    }

    private void ObjRotate()
    {
        transform.Rotate(0, rotatePower * Time.deltaTime, 0);
    }

    private void OnCollisionStay(Collision collision)
    {
        collision.transform.parent.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.parent.SetParent(null);
    }

    


}

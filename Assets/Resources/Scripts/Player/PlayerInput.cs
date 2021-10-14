using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 mousePos = Vector2.zero;
    public Vector2 MousePos
    {
        get { return mousePos; }
    }

    private float xMove;
    public float XMove
    {
        get { return xMove; }
    }

    private float zMove;
    public float ZMove
    {
        get { return zMove; }
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        xMove = Input.GetAxis("Vertical");
        zMove = Input.GetAxis("Horizontal");
    }
}

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
    private float mouseXMove;
    public float MouseXMove
    {
        get { return mouseXMove; }
    }
    private float mouseYMove;
    public float MouseYMove
    {
        get { return mouseYMove; }
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
        mouseXMove = Input.GetAxis("Mouse X");
        mouseYMove = Input.GetAxis("Mouse Y");

        xMove = Input.GetAxis("Vertical");
        zMove = Input.GetAxis("Horizontal");
    }
}

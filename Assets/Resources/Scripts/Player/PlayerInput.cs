using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float moveX = 0f;
    public float MoveX
    {
        get { return moveX; }
    }
    private float moveY = 0f;
    public float MoveZ
    {
        get { return moveY; }
    }
    private Vector2 mousePos = Vector2.zero;
    public Vector2 MousePos
    {
        get { return mousePos; }
    }
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        mousePos = Input.mousePosition;
    }
}

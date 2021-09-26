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
    public float MoveY
    {
        get { return moveY; }
    }
    private Vector2 mousePosition = Vector2.zero;
    public Vector2 MousePosition
    {
        get { return mousePosition; }
    }
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        mousePosition = Input.mousePosition;
    }
}

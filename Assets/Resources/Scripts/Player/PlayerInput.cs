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
    private float mouseX = 0f;
    public float MouseX
    {
        get { return mouseX; }
    }
    private float horizontalAngle = 0f;
    public float HorizontalAngle
    {
        get { return horizontalAngle; }
    }
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        mouseX = Input.GetAxis("Mouse X");
        horizontalAngle += mouseX;
    }
}

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
    void Update()
    {
        mousePos = Input.mousePosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverFillScript : MonoBehaviour
{
    public void SetScale(float value, float origin)
    {
        transform.localScale = new Vector3(value / origin, 1f, 1f);    
    }
}

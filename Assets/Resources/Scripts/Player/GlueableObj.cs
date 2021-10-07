using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueableObj : MonoBehaviour
{
    private Collider myCol = null;
    public float size { get; private set; }

    void Start()
    {
        myCol = GetComponent<Collider>();

        size = myCol.bounds.extents.x * myCol.bounds.extents.y * myCol.bounds.extents.z;
    }
}

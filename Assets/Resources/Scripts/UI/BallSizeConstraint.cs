using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BallSizeConstraint : MonoBehaviour
{
    private PositionConstraint constraint;
    private float offsetX;
    private float offsetY;
    private float offsetZ;
    private float colliderRadius;
    private Vector3 offset;

    public PlayerFirstObjScript playerFirst;

    void Start()
    {
        constraint = GetComponent<PositionConstraint>();
        offset = constraint.translationOffset;
        colliderRadius = playerFirst.MyCol.radius;
    }
    
    void Update()
    {
        PositionConstraint();
        Debug.Log(offset.y);
    }

    private void PositionConstraint()
    {
        
        offset = new Vector3(offset.x, offset.y, offset.z);
        constraint.translationOffset = offset;
    }

    
}

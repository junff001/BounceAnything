using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueToPlayerFirstObj : MonoBehaviour
{
    private GameManager gameManager = null;
    private GlueableObj glueableObj = null;
    private Collider myCol = null;
    
    private Vector3 originPos = Vector3.zero;

    private float distance
    {
        get { return Vector3.Distance(transform.position, gameManager.PlayerFirstObjScript.transform.position); }
    }

    private float moveSpeed = 5f;
    private float moveTime
    {
        get
        {
            return distance / moveSpeed;
        }
    }
    private float moveTimer = 0f;

    void Start()
    {
        gameManager = GameManager.Instance;
        glueableObj = GetComponent<GlueableObj>();

        originPos = transform.position;
    }

    void Update()
    {
        if (distance >= 3f)
        {
            moveTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(originPos, GetTargetPos(), moveTimer / moveTime);
        }
        else
        {
            transform.SetParent(gameManager.PlayerFirstObjScript.transform);
            gameManager.PlayerFirstObjScript.PlayerTotalSize += glueableObj.size;

            enabled = false;
        }
    }
    private Vector3 GetTargetPos()
    {
        Vector3 targetPos = Vector3.zero;

        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();

        ray.origin = transform.position;
        ray.direction = gameManager.PlayerFirstObjScript.transform.position - transform.position;

        Physics.Raycast(ray, out hit, 100f, gameManager.WhatisPlayerFirstObj);

        Debug.DrawRay(ray.origin, ray.direction, Color.red, 100f);

        targetPos = hit.point;

        return targetPos;
    }
}

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
        get { return Vector3.Distance(GetSuburbPos(), GetTargetPos()); }
    }

    private float moveSpeed = 20f;
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
        myCol = GetComponent<Collider>();

        myCol.isTrigger = true;

        originPos = transform.position;
    }

    void Update()
    {
        
        // moveTimer += Time.deltaTime;
        // transform.position = Vector3.Lerp(originPos, GetTargetPos(), moveTimer / moveTime);
        transform.position = Vector3.MoveTowards(transform.position, GetTargetPos(), moveSpeed * Time.deltaTime);        
        // if (distance >= 0.5f)
        // {
        //     moveTimer += Time.deltaTime;
        //     transform.position = Vector3.Lerp(originPos, GetTargetPos(), moveTimer / moveTime);
        // }
        // else
        // {
        //     transform.SetParent(gameManager.PlayerFirstObjScript.transform);
        //     gameObject.layer = transform.parent.gameObject.layer;
        //     myCol.isTrigger = false;

        //     gameManager.PlayerFirstObjScript.PlayerTotalSize += glueableObj.size;

        //     enabled = false;
        // }
    }
    private Vector3 GetSuburbPos()
    {
        Vector3 suburbPos = Vector3.zero;

        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();

        ray.origin = gameManager.PlayerFirstObjScript.transform.position;
        ray.direction = transform.position - gameManager.PlayerFirstObjScript.transform.position;

        Physics.Raycast(ray, out hit, 100f, gameManager.WhatIsGlueableObj);

        Debug.DrawRay(ray.origin, ray.direction, Color.black, 100f);

        suburbPos = hit.point;

        return suburbPos;
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
    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == LayerMask.GetMask("Player"))
        {
            transform.SetParent(gameManager.PlayerFirstObjScript.transform);
            gameObject.layer = transform.parent.gameObject.layer;
            myCol.isTrigger = false;
            gameManager.PlayerFirstObjScript.PlayerTotalSize += glueableObj.size;
            enabled = false;
        }
    }
}

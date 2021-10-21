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


    private bool moveEnd = false;

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
        float distance = Vector3.Distance(transform.position, GetTargetPos());


        transform.position = Vector3.MoveTowards(transform.position, GetTargetPos(), moveSpeed * Time.deltaTime);
        


        Debug.Log(myCol.bounds);
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
            // if (!other.isTrigger)
            // {
            //     return;
            // }

            float distance = Vector3.Distance(GetTargetPos(), other.transform.position) / gameManager.PlayerFirstObjScript.GetMaxSizeValue();
            
            Debug.Log(distance);

            transform.SetParent(gameManager.PlayerFirstObjScript.transform);
            gameObject.layer = transform.parent.gameObject.layer;

            if (gameManager.PlayerFirstObjScript.MoveCol.radius < distance)
            {
                gameManager.PlayerFirstObjScript.MoveCol.radius = distance;
            }

            gameManager.PlayerFirstObjScript.PlayerTotalSize += glueableObj.size;
            enabled = false;
        }
    }
}

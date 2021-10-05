using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirstObjScript : MonoBehaviour
{
    private Collider myCol = null;
    private double playerTotalSize = 0d;
    void Start()
    {
        myCol = GetComponent<Collider>();

        playerTotalSize = myCol.bounds.extents.x * myCol.bounds.extents.y * myCol.bounds.extents.z; // 모양에 따라 알맞는 크기를 구하기 힘들다고 판단, 크기를 구하는 식을 하나로 통일한다.
    }

    void Update()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        GlueableObj glueableObj = other.transform.GetComponent<GlueableObj>();

        if (glueableObj != null)
        {
            if (playerTotalSize >= glueableObj.size)
            {
                glueableObj.transform.SetParent(transform);

                playerTotalSize += glueableObj.size;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirstObjScript : MonoBehaviour
{
    private GameManager gameManager = null;
    private Collider myCol = null;
    private float playerTotalSize = 0f;
    public float PlayerTotalSize
    {
        get { return playerTotalSize; }
        set { playerTotalSize = value; }
    }
    void Start()
    {
        gameManager = GameManager.Instance;

        myCol = GetComponent<Collider>();

        playerTotalSize = myCol.bounds.extents.x * myCol.bounds.extents.y * myCol.bounds.extents.z; // 모양에 따라 알맞는 크기를 구하기 힘들다고 판단, 크기를 구하는 식을 하나로 통일한다.
        gameManager.PlayerFirstObjScript = this;
    }

    void Update()
    {

    }
    private void OnCollisionExit(Collision other)
    {
        GlueableObj glueableObj = other.transform.GetComponent<GlueableObj>();

        if (glueableObj != null)
        {
            if (playerTotalSize >= glueableObj.size)
            {
                glueableObj.gameObject.AddComponent<GlueToPlayerFirstObj>();
            }
        }
    }
}

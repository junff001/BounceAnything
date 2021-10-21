using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirstObjScript : MonoBehaviour
{
    private GameManager gameManager = null;
    private SphereCollider myCol;
    public SphereCollider MyCol
    {
        get { return myCol; }
    }
    [SerializeField]
    private SphereCollider moveCol = null;
    public SphereCollider MoveCol
    {
        get { return moveCol; }
        set { moveCol = value; }
    }
    [Header("플레이어의 시작 크기")]
    [SerializeField]
    private float playerSize = 0f;
    private float playerTotalSize = 0f;
    public float PlayerTotalSize
    {
        get { return playerTotalSize; }
        set
        {
            playerTotalSize = value;
            gameManager.Score = playerTotalSize * 100f;
        }
    }
    private float plusTotalSize = 0f;
    public float PlusPlayerTotalSize
    {
        get { return plusTotalSize; }
        set { plusTotalSize = value; }
    }
    void Start()
    {
        gameManager = GameManager.Instance;

        myCol = GetComponent<SphereCollider>();

        playerTotalSize = playerSize;
        // playerTotalSize = myCol.bounds.extents.x * myCol.bounds.extents.y * myCol.bounds.extents.z; // 모양에 따라 알맞는 크기를 구하기 힘들다고 판단, 크기를 구하는 식을 하나로 통일한다.
        gameManager.PlayerFirstObjScript = this;
    }

    void FixedUpdate()
    {
        // SetPlayerTotalSize();
    }
    private void OnCollisionEnter(Collision other)
    {
        GlueableObj glueableObj = other.transform.GetComponent<GlueableObj>();

        if (glueableObj != null && !glueableObj.socreUp)
        {
            if (playerTotalSize >= glueableObj.Size)
            {
                glueableObj.gameObject.AddComponent<GlueToPlayerFirstObj>();

                glueableObj.socreUp = true;
            }
        }
    }
    public float GetMaxSizeValue()
    {
        float a = 0f;

        if(a < transform.localScale.x)
        {
            a = transform.localScale.x;
        }

        if(a < transform.localScale.y)
        {
            a = transform.localScale.y;
        }

        if(a < transform.localScale.z)
        {
            a = transform.localScale.z;
        }

        return a;
    }
    // OnCollisionStay 처리
    // private void SetPlayerTotalSize()
    // {
    //     if (plusTotalSize > 0f)
    //     {
    //         playerTotalSize += Time.fixedDeltaTime;
    //         playerTotalSize -= Time.fixedDeltaTime;

    //         if (plusTotalSize < 0f)
    //         {
    //             playerTotalSize -= plusTotalSize;
    //             plusTotalSize = 0f;
    //         }
    //     }
    //     else
    //     {
    //         plusTotalSize = 0f;
    //     }
    // }
}

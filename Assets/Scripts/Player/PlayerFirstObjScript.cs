using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerFirstObjScript : MonoBehaviour
{
    private GameManager gameManager = null;

    private SphereCollider myCol = null;
    public SphereCollider MyCol
    {
        get { return myCol; }
    }
    [Header("플레이어 오브젝트의 첫 사이즈")]
    [SerializeField]
    private float playerSizeOrigin = 0f;
    private float playerTotalSize = 0f;
    public float PlayerTotalSize
    {
        get { return playerTotalSize; }
        set
        {
            playerTotalSize = value;
        }
    }
    private float plusTotalSize = 0f;
    public float PlusPlayerTotalSize
    {
        get { return plusTotalSize; }
        set
        {
            WhenSizeUp(value - plusTotalSize);
            plusTotalSize = value;
        }
    }
    private float plusRadius = 0f;
    public float PlusRadius
    {
        get { return plusRadius; }
        set { plusRadius = value; }
    }

    private float pastePlusRadius;
    public float PastePlusRadius
    {
        get { return pastePlusRadius; }
    }
    public event Action<float> WhenSizeUp;

    private void Awake()
    {
        myCol = GetComponent<SphereCollider>();

        WhenSizeUp = (e) =>
        {
            //Debug.Log("aaaaaaaaaaaaaaaaaaaaaa"+e);
        };
    }

    void Start()
    {
        gameManager = GameManager.Instance;


        playerTotalSize = playerSizeOrigin;

        //playerTotalSize = myCol.bounds.extents.x * myCol.bounds.extents.y * myCol.bounds.extents.z; 
        //// 모양에 따라 알맞는 크기를 구하기 힘들다고 판단, 크기를 구하는 식을 하나로 통일한다;
    }

    void FixedUpdate()
    {
        SetPlayerTotalSize();
        SetPlayerColliderRadius();

        gameManager.Score = (playerTotalSize - playerSizeOrigin) * 100f; // 현재점수 갱신
    }

    private void OnTriggerEnter(Collider other)
    {
        GlueableObj glueableObj = other.transform.GetComponent<GlueableObj>();

        if (glueableObj != null && !glueableObj.socreUp)
        {
            if (playerTotalSize >= glueableObj.SizeOrigin)
            {
                //Debug.Log("p" + playerTotalSize);
                //Debug.Log("t" + glueableObj.SizeOrigin);

                glueableObj.gameObject.AddComponent<GlueToPlayerFirstObj>();

                glueableObj.socreUp = true;
            }
            else
            {
                other.isTrigger = false;
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        GlueableObj glueableObj = other.transform.GetComponent<GlueableObj>();

        if (glueableObj != null && !glueableObj.socreUp)
        {
            if (playerTotalSize >= glueableObj.SizeOrigin)
            {
                other.collider.isTrigger = true;
            }
        }
    }
    // OnCollisionStay 처리
    private void SetPlayerTotalSize()
    {
        if (plusTotalSize > 0f)
        {
            float pastePlusTotalSize = plusTotalSize;

            plusTotalSize -= Time.fixedDeltaTime;
            playerTotalSize += Time.fixedDeltaTime;

            if (plusTotalSize < 0f)
            {
                playerTotalSize += pastePlusTotalSize - Time.fixedDeltaTime;
                plusTotalSize = 0f;
            }
        }
        else
        {
            plusTotalSize = 0f;
        }
    }
    private void SetPlayerColliderRadius()
    {
        if (plusRadius > 0f)
        {
            pastePlusRadius = plusRadius;

            plusRadius -= Time.fixedDeltaTime;
            myCol.radius += Time.fixedDeltaTime;

            if (plusRadius < 0f)
            {
                myCol.radius += pastePlusRadius - Time.fixedDeltaTime;
                plusRadius = 0f;
            }

            //float addValue = myCol.radius - 0.5f;
            //myCol.center = new Vector3(0, addValue / 2, 0);
        }
        else
        {
            plusRadius = 0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueToPlayerFirstObj : MonoBehaviour
{
    private GameManager _gameManager = null;
    public GameManager gameManager
    {
        get
        {
            if (_gameManager == null)
            {
                _gameManager = GameManager.Instance;
            }

            return _gameManager;
        }
    }

    private GlueableObj glueableObj = null;
    private Collider myCol = null;



    private Vector3 originPos = Vector3.zero;

    private float distance
    {
        get { return Vector3.Distance(GetSuburbPos(), GetTargetPos()); } // 오브젝트 
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
    private bool sizeUp = false;

    private void Awake() 
    {
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

    private Vector3 GetSuburbPos() //공 밖 오브젝트 RaycastHit으로 체크
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

    private Vector3 GetTargetPos() // 이 오브젝트에서 플레이어 오브젝트한테 Ray를 쏨, hit.point를 반환
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

    private void OnTriggerEnter(Collider other) // 플레이어의 Collider중 isTrigger가 true인 Collider와 충돌했을 때
    {
        if (1 << other.gameObject.layer == LayerMask.GetMask("Player")) // 플레이어에게로 날아가는 오브젝트가 플레이어와 충돌(이동에 쓰이는 구 형의 SphereCollider가 아닌, IsTrigger == true인 Collider와 충돌 체크
        {
            if (!other.isTrigger || sizeUp)
            {
                return;
            }
            
            sizeUp = true;
            
            float distance = Vector3.Distance(transform.position, other.transform.position); // 붙었을 때 PlayerObject의 중심까지의 거리를 잰다.

            transform.SetParent(gameManager.PlayerFirstObjScript.transform);
            gameObject.layer = transform.parent.gameObject.layer;

            if (gameManager.PlayerFirstObjScript.MyCol.radius < distance) // 잰 거리가 이동용으로 쓰이는 SphereCollider의 radius값보다 크면,
            {
                gameManager.PlayerFirstObjScript.PlusRadius += distance - gameManager.PlayerFirstObjScript.MyCol.radius; // distance와 SphereCollider의 radius값의 차이만큼 PlusRadius에 값을 더해준다.
            }                                                                                                            // radius는 PlusRadius의 값만큼 서서히 증가한다.

            if (glueableObj != null)
            {
                float upSize = glueableObj.SizePlus;
                gameManager.PlayerFirstObjScript.PlusPlayerTotalSize += upSize; // PlusPlayerTotalSize는 PlusRadius와 동일한 역할을 한다.
                gameManager.SpawnGetScoreText(upSize * 100f);
            }
            
            myCol.isTrigger = true;
            
            enabled = false;
        }
    }
}

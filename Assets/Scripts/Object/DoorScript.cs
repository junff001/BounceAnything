using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private GameManager gameManager = null;

    [SerializeField]
    private float openScore = 0f;

    [SerializeField]
    private float speed = 0f;
    private float moveTime = 0f;
    private float moveTimer = 0f;
    [Header("targetPosition의 (0, 0, 0)값은 이 오브젝트의 위치와 동일하다.")]
    [SerializeField]
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 originPosition = Vector3.zero;


    void Start()
    {
        gameManager = GameManager.Instance;

        originPosition = transform.position;
        targetPosition += transform.position;

        GetMoveTime();
    }

    void Update()
    {
        if(gameManager.Score >= openScore)
        {
            Open();
        }
    }
    private void GetMoveTime()
    {
        float distance = Vector3.Distance(originPosition, targetPosition);

        moveTime = distance / speed;
    }
    private void Open()
    {
        if(moveTimer < moveTime)
        {
            moveTimer += Time.deltaTime;

            transform.position = Vector3.Lerp(originPosition, targetPosition, moveTimer / moveTime);
        }
    }
}

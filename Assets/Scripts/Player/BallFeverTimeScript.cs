using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFeverTimeScript : MonoBehaviour
{
    private PlayerFirstObjScript playerFirstObjScript = null;
    private BallController ballController = null;

    [SerializeField]
    private float feverMax = 5f;
    [Header("공이 피버모드일 때의 스피드")]
    [SerializeField]
    private float feverSpeed = 0f;
    [Header("공이 피버모드가 됐을 때 피버 게이지가 줄어드는 속도")]
    [SerializeField]
    private float feverMinusSpeed = 1.5f;
    [SerializeField]
    private float feverUpSpeed = 2f;

    private float fever = 0f;
    private float plusFever = 0f;

    private float playerSpeedOrigin = 0f;

    private bool isFever = false;

    private void Awake()
    {
        playerFirstObjScript = GetComponent<PlayerFirstObjScript>();
        ballController = GetComponent<BallController>();

        playerFirstObjScript.WhenSizeUp += (size) =>
        {
            plusFever += size;
        };
    }

    void Update()
    {
        SetFever();
        CheckFever();
    }

    private void CheckFever()
    {
        if (fever >= feverMax)
        {
            isFever = true;

            playerSpeedOrigin = ballController.MoveSpeed;
            ballController.MoveSpeed = feverSpeed;

            plusFever = 0f;
            fever = feverMax;
        }
    }

    private void SetFever()
    {
        if (isFever)
        {
            fever -= Time.deltaTime * feverMinusSpeed;

            if(fever <= 0f)
            {
                fever = 0f;

                ballController.MoveSpeed = playerSpeedOrigin;
                isFever = false;
            }
        }
        else
        {
            if (plusFever > 0f)
            {
                plusFever -= Time.deltaTime * feverUpSpeed;
                fever += Time.deltaTime * feverUpSpeed;

                if (plusFever <= 0f)
                {
                    fever += plusFever;
                    plusFever = 0f;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFeverTimeScript : MonoBehaviour
{
    private SpawnAfterImage spawnAfterImage = null;
    private PlayerFirstObjScript playerFirstObjScript = null;
    private BallController ballController = null;
    private FeverFillScript feverFillScript = null;
    private FeverFillScript FeverFillScript
    {
        get
        {
            if (feverFillScript == null)
            {
                feverFillScript = FindObjectOfType<FeverFillScript>();

                if (feverFillScript == null)
                {
                    Debug.LogError("There is no FeverFillScript!");
                }
            }

            return feverFillScript;
        }
    }

    [SerializeField]
    private float feverMax = 5f;
    [Header("공이 피버모드일 때의 스피드")]
    [SerializeField]
    private float feverSpeed = 0f;
    [Header("공이 '피버모드'가 됐을 때 피버 '게이지'가 줄어드는 속도")]
    [SerializeField]
    private float feverMinusSpeedWhenIsFever = 1.5f;
    [Header("피버 '게이지'가 차오르는 속도")]
    [SerializeField]
    private float feverUpSpeed = 2f;
    [Header("잔상이 소환되는 주기")]
    [SerializeField]
    private float afterImageSpawnDelay = 0.5f;

    private float fever = 0f;
    private float plusFever = 0f;

    private float playerSpeedOrigin = 0f;

    private bool isFever = false;

    private void Awake()
    {
        playerFirstObjScript = GetComponent<PlayerFirstObjScript>();
        ballController = GetComponent<BallController>();
        spawnAfterImage = GetComponent<SpawnAfterImage>();

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
            StartCoroutine(SpawnAfterImage());

            fever -= Time.deltaTime * feverMinusSpeedWhenIsFever;

            if (fever <= 0f)
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

        FeverFillScript.SetScale(fever, feverMax);
    }
    private IEnumerator SpawnAfterImage()
    {
        spawnAfterImage.Spawn(transform.position, playerFirstObjScript.MyCol.radius, transform.rotation);

        yield return new WaitForSeconds(afterImageSpawnDelay);
    }
}

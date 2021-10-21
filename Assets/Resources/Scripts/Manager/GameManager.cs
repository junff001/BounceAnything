using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject temp = new GameObject("GameManager");
                    instance = temp.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }

    [SerializeField]
    private LayerMask whatIsPlayerFirstObj;
    public LayerMask WhatisPlayerFirstObj
    {
        get { return whatIsPlayerFirstObj; }
    }
    [SerializeField]
    private LayerMask whatIsGlueableObj;
    public LayerMask WhatIsGlueableObj
    {
        get { return whatIsGlueableObj; }
    }

    [SerializeField]
    private GameObject playerPrefab = null;
    private GameObject currentPlayerObj = null;
    public GameObject CurrentPlayerObj
    {
        get { return currentPlayerObj; }
    }

    [SerializeField]
    private Transform playerSpawnTrm = null;
    [SerializeField]
    private Transform glueableObjParent = null;
    public Transform GlueableObjParent
    {
        get { return glueableObjParent; }
    }

    [SerializeField]
    private GameObject StartPanel = null;
    [SerializeField]
    private GameObject EndPanel = null;

    [SerializeField]
    private Text scoreText = null;
    [SerializeField]
    private Text gameClearText = null;
    [SerializeField]
    private Text clearTimeText = null;
    [SerializeField]
    private Text clearScoreText = null;

    private bool gameStart = false;
    public bool GameStart
    {
        get { return gameStart; }
    }

    private bool gameClear = false;

    private float score = 0f;
    public float Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreText.text = "Score: " + value;
        }
    }

    [SerializeField]
    private float targetScore = 1300f;
    public float TargetScore
    {
        get { return targetScore; }
        set { targetScore = value; }
    }

    private float totalSec = 0f;
    private int totalMin = 0;

    private PlayerFirstObjScript playerFirstObjScript = null;
    public PlayerFirstObjScript PlayerFirstObjScript
    {
        get
        {
            return playerFirstObjScript;
        }
        set
        {
            playerFirstObjScript = value;
        }
    }

    public event Action StartGame; // 게임이 시작할 때 실행됌. 여러 수치를 초기화 할 때 사용중
    public event Action EndGame; // 게임이 끝났을 때 점수 합산, 클리어 시간 기록 등을 할때 씀
    public event Action RestartGame; // 게임 다시시작할 때 실행, 오브젝트들의 위치 초기화 등을 할 때 사용

    private void Awake()
    {
        StartGame = () =>
        {
            totalSec = 0f;
            totalMin = 0;
            score = 0;

            scoreText.text = "Score: " + score;

            gameStart = true;
            gameClear = false;

            currentPlayerObj = Instantiate(playerPrefab, playerSpawnTrm);

            StartPanel.SetActive(false);
            scoreText.gameObject.SetActive(true);
        };

        EndGame = () =>
        {
            gameStart = false;

            if (gameClear)
            {
                gameClearText.text = "Game Clear!";
            }
            else
            {
                gameClearText.text = "You Lose!";
            }

            clearTimeText.text = totalMin + "분 " + totalSec + "초";

            clearScoreText.text = score + "점";

            EndPanel.SetActive(true);
            scoreText.gameObject.SetActive(false);
        };

        RestartGame = () =>
        {
            EndPanel.SetActive(false);
            StartPanel.SetActive(true);
        };
    }
    void Start()
    {

    }
    void Update()
    {
        // if(Input.GetKeyUp)
        if (gameStart)
        {
            totalSec += Time.deltaTime;

            if (totalSec >= 60f)
            {
                totalSec -= 60f;
                totalMin++;
            }
        }

        if (score > targetScore && gameStart)
        {
            gameClear = true;

            EndGame();
        }
    }
    public void OnClickStartBtn() // StartButton을 눌렀을 때 실행
    {
        StartGame();
    }
    public void OnClickReStartBtn() // RestartButton을 눌렀을 때 실행
    {
        RestartGame();

        Destroy(currentPlayerObj);
    }
}

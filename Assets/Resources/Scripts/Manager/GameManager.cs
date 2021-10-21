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
    
    [Header("점수가 오르는 기준: 붙은 오브젝트의 크기 X 100")]
    [SerializeField]
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

    public event Action StartGame;
    public event Action EndGame;
    public event Action RestartGame;

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
    public void OnClickStartBtn()
    {
        StartGame();
    }
    public void OnClickReStartBtn()
    {
        RestartGame();

        Destroy(currentPlayerObj);
    }
}

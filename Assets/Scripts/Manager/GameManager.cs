using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ScriptHelper
{
    public static List<T> ToList<T>(this IEnumerable<T> list)
    {
        List<T> returnList = new List<T>();

        foreach (T item in list)
        {
            returnList.Add(item);
        }

        return returnList;
    }
    public static List<T> ToList<T>(this IEnumerable<T> list, Action<T> action)
    {
        List<T> returnList = new List<T>();

        foreach (T item in list)
        {
            returnList.Add(item);
        }

        return returnList;
    }
    public static List<T> SumList<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        List<T> returnList = new List<T>();

        foreach (T item in list1)
        {
            returnList.Add(item);
        }

        foreach (T item in list2)
        {
            returnList.Add(item);
        }

        return returnList;
    }
    public static int GetLayer<LayerMask>(this int mask) // GetLayerMask를 통해 얻는 layer값을 gameObject.layer에 대입할 때 이 함수로 값을 변환하여 대입시킨다.
    {
        int result = 0;
        int a = mask;

        while (true) {
            a /= 2;

            result++;

            if (a == 1) {
                break;
            }
        }

        return result;
    }
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab = null;
    [SerializeField]
    private GameObject StartPanel = null;
    [SerializeField]
    private GameObject EndPanel = null;
    [SerializeField]
    private GameObject GetScoreText = null;
    [SerializeField]
    private GameObject FeverGauge = null;
    [SerializeField]
    private Transform playerSpawnTrm = null;
    [SerializeField]
    public Transform respawnTrm = null;
    [SerializeField]
    private RectTransform cursorTrm = null;
    [SerializeField]
    private RectTransform getScoreTextTrm = null;
    [SerializeField]
    private Image cursorImg = null;
    [SerializeField]
    private Text scoreText = null;
    [SerializeField]
    private Text gameClearText = null;
    [SerializeField]
    private Text clearTimeText = null;
    [SerializeField]
    private Text clearScoreText = null;
    [Header("리스폰 높이")]
    [SerializeField]
    private float respwnHeight = 1.5f;
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
    private Transform glueableObjParent = null;
    public Transform GlueableObjParent
    {
        get { return glueableObjParent; }
    }
    [SerializeField]
    private float targetScore = 1300f;
    public float TargetScore
    {
        get { return targetScore; }
        set { targetScore = value; }
    }

    private PoolManager poolManager = null;
    private PlayerFirstObjScript pFirst;
    private Sprite newCursor = null;
    private Sprite newCursor_Clicked = null;
    private int totalMin = 0;
    private float totalSec = 0f;
    private bool gameClear = false;
    private float radius;
    

    private BallController ballCon;

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
    private GameObject currentPlayerObj = null;
    public GameObject CurrentPlayerObj
    {
        get { return currentPlayerObj; }
        set { currentPlayerObj = value; }
    }
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
    private float score = 0f;
    public float Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreText.text = "Score: " + string.Format("{0:0.##}", score);
        }
    }
    private bool gameStart = false;
    public bool GameStart
    {
        get { return gameStart; }
    }

    public event Action StartGame; // 게임이 시작할 때 실행됌. 여러 수치를 초기화 할 때 사용중
    public event Action EndGame; // 게임이 끝났을 때 점수 합산, 클리어 시간 기록 등을 할때 씀
    public event Action RestartGame; // 게임 다시시작할 때 실행, 오브젝트들의 위치 초기화 등을 할 때 사용
    public event Action RespwnPlayer;

    private void Awake()
    {
        poolManager = PoolManager.Instance;
 
        // 람다로 초기화
        StartGame = () => 
        {
            totalSec = 0f;
            totalMin = 0;
            score = 0;

            scoreText.text = "Score: " + score;

            gameStart = true;
            gameClear = false;

            currentPlayerObj = Instantiate(playerPrefab, playerSpawnTrm); // 게임 매니저 오브젝트 안에서 생성
            ballCon = currentPlayerObj.GetComponent<BallController>();
            playerFirstObjScript = currentPlayerObj.GetComponent<PlayerFirstObjScript>();
            pFirst = currentPlayerObj.GetComponent<PlayerFirstObjScript>();
            radius = pFirst.MyCol.radius;

            if (playerFirstObjScript == null)
            {
                playerFirstObjScript = currentPlayerObj.GetComponentInChildren<PlayerFirstObjScript>();
            }

            StartPanel.SetActive(false);
            scoreText.gameObject.SetActive(true);
            FeverGauge.SetActive(true);

            Cursor.lockState = CursorLockMode.Locked;
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
            FeverGauge.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
        };

        RestartGame = () =>
        {
            EndPanel.SetActive(false);
            StartPanel.SetActive(true);
        };

        RespwnPlayer = () => {
            if (radius < pFirst.MyCol.radius) {
                respawnTrm.position += new Vector3(0, respwnHeight, 0);
                radius = pFirst.MyCol.radius;
            }
            currentPlayerObj.transform.position = respawnTrm.position;
        };
    }
    void Start()
    {
        Sprite[] newCursors = Resources.LoadAll<Sprite>("Image/Cursors/HandIcons");

        newCursor = newCursors[0];
        newCursor_Clicked = newCursors[1];

        Cursor.visible = false;

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

        cursorTrm.position = Input.mousePosition;

        if (!gameStart)
        {
            if(!cursorImg.gameObject.activeSelf)
            {
                cursorImg.gameObject.SetActive(true);
            }
            
            if (Input.GetMouseButton(0))
            {
                cursorImg.sprite = newCursor_Clicked;
            }
            else
            {
                cursorImg.sprite = newCursor;
            }
        }
        else
        {
            cursorImg.gameObject.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            RespwnPlayer();
        }
    }

    private void aa()
    {
        if (0 == 1) {
            
        }
    
    }

    private void F()
    {
    }
	public void SpawnGetScoreText(float score)
    {
        if (poolManager.TextObjQueue.Count > 0)
        {
            GameObject temp = poolManager.TextObjQueue.Dequeue();
            Text text = temp.GetComponent<Text>();

            if (text == null)
            {
                Debug.LogError(temp.name + " has no Text.");
            }

            text.text = "+ " + score;

            text.rectTransform.position = getScoreTextTrm.position;

            temp.SetActive(true);
        }
        else
        {
            GameObject temp = Instantiate(GetScoreText, getScoreTextTrm);
            Text text = temp.GetComponent<Text>();

            if (text == null)
            {
                Debug.LogError(temp.name + " has no Text.");
            }

            text.text = "+ " + score;

            text.rectTransform.position = getScoreTextTrm.position;
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

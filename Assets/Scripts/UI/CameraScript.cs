using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameManager gameManager = null;
    private PlayerInput playerInput = null;
    private PlayerFirstObjScript playerFirstObjScript = null;
    [SerializeField]
    private Transform playerTrm = null;

    [SerializeField]
    private Vector3 offset = Vector3.zero;

    [SerializeField]
    private float maxY = 45f;
    [SerializeField]
    private float minY = 10f;
    [SerializeField]
    private float cameraRange = 5f;
    [SerializeField]
    private float cameraHeight = 10f;

    [SerializeField]
    private float mouseXSpeed = 1f;

    private float angleY = 0f;
    private float angleXZ = 0f;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void Start()
    {
        gameManager.StartGame += () =>
        {
            Debug.Log(gameManager.PlayerFirstObjScript);
            playerInput = FindObjectOfType<PlayerInput>();

            playerFirstObjScript = gameManager.PlayerFirstObjScript;

            playerTrm = playerFirstObjScript.transform;
        };


    }
    void Update()
    {
        if (gameManager.GameStart) // 카메라 시점관련 코드
        {
            Vector3 cameraPos = new Vector3(playerTrm.position.x - transform.forward.x * cameraRange, playerTrm.position.y, playerTrm.position.z - transform.forward.z * cameraRange);
            transform.position = cameraPos;

            angleY += playerInput.MouseXMove * mouseXSpeed;
            angleXZ += playerInput.MouseYMove;

            angleXZ = Mathf.Clamp(angleXZ, minY, maxY);

            transform.position += Quaternion.Euler(angleXZ, angleY, angleXZ) * new Vector3(offset.x, offset.y, -cameraHeight - (playerFirstObjScript.PlayerTotalSize / 10f));
            transform.LookAt(playerTrm.transform);
        }
    }
}

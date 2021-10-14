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
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void Start()
    {
        gameManager.StartGame += () =>
        {
            playerInput = FindObjectOfType<PlayerInput>();
            playerFirstObjScript = FindObjectOfType<PlayerFirstObjScript>();

            playerTrm = gameManager.CurrentPlayerObj.transform;
        };
    }
    void Update()
    {
        if (gameManager.GameStart)
        {
            Vector3 cameraPos = new Vector3(playerTrm.position.x - transform.forward.x * cameraRange, playerTrm.position.y, playerTrm.position.z - transform.forward.z * cameraRange);
            transform.position = cameraPos;

            float angleY = (playerInput.MousePos.x / (Mathf.PI * 2));
            float angleXZ = (playerInput.MousePos.y / (Mathf.PI * 2));

            angleXZ = Mathf.Clamp(angleXZ, minY, maxY);

            transform.position += Quaternion.Euler(angleXZ, angleY, angleXZ) * new Vector3(offset.x, offset.y, -cameraHeight - playerFirstObjScript.PlayerTotalSize);
            transform.LookAt(playerTrm.transform);
        }
    }
}

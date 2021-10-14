using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
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

    void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        playerFirstObjScript = FindObjectOfType<PlayerFirstObjScript>();
    }
    void Update()
    {

        Vector3 cameraPos = new Vector3(playerTrm.position.x - transform.forward.x * cameraRange, playerTrm.position.y, playerTrm.position.z - transform.forward.z * cameraRange);
        transform.position = cameraPos;

        float angleY = (playerInput.MousePos.x / (Mathf.PI * 2));
        float angleXZ = (playerInput.MousePos.y / (Mathf.PI * 2));

        angleXZ = Mathf.Clamp(angleXZ, minY, maxY);

        transform.position += Quaternion.Euler(angleXZ, angleY, angleXZ) * new Vector3(offset.x, offset.y, - cameraHeight - playerFirstObjScript.PlayerTotalSize);
        transform.LookAt(playerTrm.transform);        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private PlayerInput playerInput = null;
    [SerializeField]
    private Transform playerTrm = null;
    [SerializeField]
    private float cameraRange = 5f; 
    [SerializeField]
    private float cameraHeight = 10f;

    void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, playerInput.MousePos.x, transform.rotation.z);

        Vector3 cameraPos = new Vector3(playerTrm.position.x - transform.forward.x * cameraRange, playerTrm.position.y + cameraHeight, playerTrm.position.z - transform.forward.z * cameraRange);
        transform.position = cameraPos;

        transform.LookAt(playerTrm);
    }
}

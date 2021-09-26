using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform playerTrm = null;
    [SerializeField]
    private float cameraRange = 5f;

    void Start()
    {
        
    }
    void Update()
    {
        Debug.Log(playerTrm.forward);
        Vector3 cameraPos = new Vector3(playerTrm.position.x * playerTrm.forward.x, cameraRange, playerTrm.position.z - cameraRange);
        transform.position = cameraPos;

        transform.LookAt(playerTrm);
    }
}

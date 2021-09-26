using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform playerTrm = null;
    [SerializeField]
    private float camearaRange = 5f;

    void Start()
    {
        
    }
    void Update()
    {
        Vector3 cameraPos = new Vector3(playerTrm.position.x, camearaRange, playerTrm.position.z - camearaRange);
        transform.position = cameraPos;

        transform.LookAt(playerTrm);
    }
}

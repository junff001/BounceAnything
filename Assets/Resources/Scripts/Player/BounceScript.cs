using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    private PlayerInput plyaerInput = null;
    private Rigidbody rigid = null;

    [SerializeField]
    private LayerMask whatIsGround;

    private Vector3 currentPosition = Vector3.zero;


    [SerializeField]
    private float jumpPower = 5f;
    [SerializeField]
    private float moveSpeed = 2f;
    void Start()
    {
        plyaerInput = GetComponent<PlayerInput>();

        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        currentPosition = transform.position;

        currentPosition += (new Vector3(-(plyaerInput.MoveX * moveSpeed), 0f, plyaerInput.MoveY * moveSpeed) + transform.forward) * Time.deltaTime;

        rigid.rotation = Quaternion.Euler(rigid.rotation.x, plyaerInput.MousePos.x, rigid.rotation.z);

        transform.position = currentPosition;
    }
    private void OnCollisionEnter(Collision other)
    {
        int index = 2 << other.gameObject.layer - 1;

        if (index == LayerMask.GetMask("GROUND"))
        {
            rigid.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }
        // point와 이 오브젝트의 위치에 따라서 AddForce가 달라지도록 하자
    }
}

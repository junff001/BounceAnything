using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private GameManager gameManager = null;
    private PlayerInput playerInput = null;
    private Rigidbody rigid = null;

    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float rotationSpeed = 3f;
    [SerializeField]
    private float jumpHeight = 10f;

    private float totalTime = 0f;

    private bool isGround;

    void Start()
    {
        gameManager = GameManager.Instance;
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        isGround = true;
    }

    void FixedUpdate()
    {
        BallMove();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void BallMove()
    {
        if (isGround) //Ball 점프 관련
        {
            rigid.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
            isGround = false;
        }
        Debug.Log(playerInput.ZMove);
        rigid.AddForce(new Vector3(playerInput.XMove, 0, playerInput.ZMove) * moveSpeed); //Ball 이동 관련
    }
}


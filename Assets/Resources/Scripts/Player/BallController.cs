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
        //isGround = false;
    }

    void FixedUpdate()
    {
        BallMove();
    }

    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision) // OnCollisionEnter���� OnCollisionStay�� �ٲ㺽
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }   
    }
    private void OnCollisionExit(Collision collision) // OnCollisionExit�� isGround = false �ǵ��� �����غ�
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    private void BallMove()
    {
        if (isGround) //Ball ���� ����
        {
            rigid.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
            
            if (playerInput.XMove > 0)  //Ball �̵� ����
            {
                rigid.AddForce(Camera.main.gameObject.transform.forward * moveSpeed, ForceMode.Impulse);
            }
            else if (playerInput.XMove < 0)
            {
                rigid.AddForce(-Camera.main.gameObject.transform.forward * moveSpeed, ForceMode.Impulse);
            }
            if (playerInput.ZMove > 0)
            {
                rigid.AddForce(Camera.main.gameObject.transform.right * moveSpeed, ForceMode.Impulse);
            }
            else if (playerInput.ZMove < 0)
            {
                rigid.AddForce(-Camera.main.gameObject.transform.right * moveSpeed, ForceMode.Impulse);
            }
        }
    }
}

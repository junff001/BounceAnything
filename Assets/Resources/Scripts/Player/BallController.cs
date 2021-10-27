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
    [SerializeField]
    private const float maxSpeed = 11f;

    private float totalTime = 0f;
    private bool isGround;
    
    void Start()
    {
        gameManager = GameManager.Instance;

        gameManager.RespwnPlayer += () =>
        {
            rigid.velocity = Vector3.zero;
        };

        rigid = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        BallMove();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }   
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    private void BallMove()
    {
        if (isGround) //Ball 이동관련
        {
            if (playerInput.XMove > 0)
            {
                rigid.AddForce(Camera.main.gameObject.transform.forward * moveSpeed, ForceMode.Impulse);
            }
            else if (playerInput.XMove < 0)
            {
                rigid.AddForce(-Camera.main.gameObject.transform.forward * moveSpeed, ForceMode.Impulse);
            }
            if (playerInput.ZMove > 0) //파란축 
            {
                rigid.AddForce(Camera.main.gameObject.transform.right * moveSpeed, ForceMode.Impulse);
            }
            else if (playerInput.ZMove < 0)
            {
                rigid.AddForce(-Camera.main.gameObject.transform.right * moveSpeed, ForceMode.Impulse);
            }

            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, maxSpeed); //공 속도제한
            Debug.Log(string.Format("BallVelocity:{0}", rigid.velocity));
        }
    }
}


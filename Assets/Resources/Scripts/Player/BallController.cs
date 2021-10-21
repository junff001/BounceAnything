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

    // AddForce ������ �Ŵ� ����
    public float minValue = 1f;
    public float maxValue = 10f;

    const float maxSpeed = 11f;


    void Start()
    {
        gameManager = GameManager.Instance;
        rigid = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        BallMove();
    }

    void Update()
    {
        
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
            if (playerInput.XMove > 0)  //Ball 힘을 가하고 제한
            {
                rigid.AddForce(Camera.main.gameObject.transform.forward * moveSpeed, ForceMode.Impulse);
                //rigid.velocity = new Vector3(Mathf.Clamp(moveSpeed, minValue, maxValue), 
                //rigid.velocity.y, rigid.velocity.z);

                Debug.Log(string.Format("x:{0}, z:{1}", rigid.velocity.x, rigid.velocity.z));
            }
            else if (playerInput.XMove < 0)
            {
                rigid.AddForce(-Camera.main.gameObject.transform.forward * moveSpeed, ForceMode.Impulse);
            }
            if (playerInput.ZMove > 0) // �Ķ���  
            {
                rigid.AddForce(Camera.main.gameObject.transform.right * moveSpeed, ForceMode.Impulse);
            }
            else if (playerInput.ZMove < 0)
            {
                rigid.AddForce(-Camera.main.gameObject.transform.right * moveSpeed, ForceMode.Impulse);
            }

            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, maxSpeed);
        }
    }
}


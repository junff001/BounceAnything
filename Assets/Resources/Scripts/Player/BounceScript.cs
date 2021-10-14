using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    private GameManager gameManager = null;
    private PlayerInput playerInput = null;
    private Rigidbody rigid = null;

    [SerializeField]
    private LayerMask whatIsJumpable;

    private Vector3 currentPosition = Vector3.zero;

    [SerializeField]
    private float jumpSpeed = 5f;
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float rotationSpeed = 3f;
   
    private float totalTime = 0f;

    void Start()
    {
        gameManager = GameManager.Instance;

        playerInput = GetComponent<PlayerInput>();

        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        totalTime += Time.deltaTime * rotationSpeed;

        currentPosition = transform.position;

        currentPosition += Camera.main.transform.forward * moveSpeed * Time.deltaTime; 

        rigid.rotation = Quaternion.Euler((totalTime * Mathf.Rad2Deg), playerInput.MousePos.x, rigid.rotation.z);

        transform.position = currentPosition;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (whatIsJumpable == (whatIsJumpable | 1 << other.gameObject.layer))
        {
            rigid.velocity = Vector3.zero;
            rigid.AddForce((new Vector3(0f, transform.position.y, 0f) + Vector3.up) * jumpSpeed, ForceMode.Impulse);   
        }
    }
}

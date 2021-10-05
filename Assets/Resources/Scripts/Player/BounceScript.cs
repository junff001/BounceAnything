using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    private PlayerInput playerInput = null;
    private Rigidbody rigid = null;

    [SerializeField]
    private LayerMask whatIsGround;

    private Vector3 currentPosition = Vector3.zero;
    private Vector3 originJumpPos = Vector3.zero;
    private Vector3 jumpTargetPos = Vector3.zero;

    private bool jumping = false;
    private bool jumpDown = false;

    [SerializeField]
    private float jumpSpeed = 5f;
    [SerializeField]
    private float jumpRange = 5f;
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float rotationSpeed = 3f;

    private float totalTime = 0f;
    private float jumpTime = 0f;
    private float jumpTimer = 0f;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        totalTime += Time.deltaTime * rotationSpeed;
        currentPosition = transform.position;

        currentPosition += (new Vector3((playerInput.MoveX * moveSpeed), 0f, playerInput.MoveZ * moveSpeed) + Vector3.forward) * Time.deltaTime;

        rigid.rotation = Quaternion.Euler((totalTime * Mathf.Rad2Deg), playerInput.MousePos.x, rigid.rotation.z);

        if (jumping)
        {
            if (jumpTimer < jumpTime && !jumpDown)
            {
                jumpTimer += Time.deltaTime;

                JumpPosSet();

                currentPosition = Vector3.Lerp(originJumpPos, jumpTargetPos, jumpTimer / jumpTime);

                if (jumpTimer >= jumpTime)
                {
                    jumpTimer = jumpTime;

                    jumpDown = true;
                }
            }
            else if (jumpDown)
            {
                if (jumpTimer >= 0f)
                {
                    jumpTimer -= Time.deltaTime;

                    JumpPosSet();

                    currentPosition = Vector3.Lerp(originJumpPos, jumpTargetPos, jumpTimer / jumpTime);
                }
                else
                {
                    jumpDown = false;
                    jumping = false;
                }
            }
        }

        transform.position = currentPosition;
    }

    private void JumpPosSet()
    {
        originJumpPos.x = currentPosition.x;
        originJumpPos.z = currentPosition.z;

        jumpTargetPos.x = currentPosition.x;
        jumpTargetPos.z = currentPosition.z;
    }

    private void OnCollisionStay(Collision other)
    {
        int index = 2 << other.gameObject.layer - 1;

        if (index == LayerMask.GetMask("GROUND"))
        {
            // rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            if (!jumping)
            {
                jumping = true;

                jumpTime = jumpRange / jumpSpeed;

                Jump();
            }

        }
        // point와 이 오브젝트의 위치에 따라서 AddForce가 달라지도록 하자
    }
    private void Jump()
    {
        jumpTimer = 0f;
        originJumpPos = currentPosition;

        jumpTargetPos = currentPosition;
        jumpTargetPos.y += jumpRange;
    }
}

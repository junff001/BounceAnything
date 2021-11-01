using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 되도록 객체 기준으로 기능 합치기
// 연관성 없는 객체일때 cs 나누기

public class BallController : MonoBehaviour
{
    private GameManager gameManager = null;
    private PlayerInput playerInput = null;
    private PlayerFirstObjScript playerFirst;
    private Rigidbody rigid = null;

    [SerializeField]
    private LayerMask whatIsMovable;
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float rotationSpeed = 3f;
    [SerializeField]
    private float jumpHeight = 10f;
    [SerializeField]
    private const float maxSpeed = 11f;

    private float totalTime = 0f;
    private bool canMove;
    private float yDelta;
    private float ColRadius;
    public RectTransform sizeCanvas;
    public Image sizeImage;
    private Vector3 startPos;

    void Start()
    {
        gameManager = GameManager.Instance;

        gameManager.RespwnPlayer += () => {
            rigid.velocity = Vector3.zero;
        };

        rigid = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerFirst = GetComponent<PlayerFirstObjScript>();
        startPos = transform.position;
        yDelta = transform.position.y - sizeCanvas.position.y; // 공과 캔버스의 사잇값 (높이)

    }

    void FixedUpdate() 
    {
        BallMove();
    }

    void Update()
    {
        CanvasFollow();
        ImageSizeUp();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Movable")) {
            canMove = true;
        }   
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Movable")) {
            canMove = false;
        }
    }

    private void BallMove() 
    {
        if (canMove) { //Ball 이동관련
            if (playerInput.XMove > 0) {
                rigid.AddForce(Camera.main.gameObject.transform.forward * moveSpeed, ForceMode.Impulse);
            } else if (playerInput.XMove < 0) {
                rigid.AddForce(-Camera.main.gameObject.transform.forward * moveSpeed, ForceMode.Impulse);
            } if (playerInput.ZMove > 0) {
                rigid.AddForce(Camera.main.gameObject.transform.right * moveSpeed, ForceMode.Impulse);
            } else if (playerInput.ZMove < 0) {
                rigid.AddForce(-Camera.main.gameObject.transform.right * moveSpeed, ForceMode.Impulse);
            }
            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, maxSpeed); //공 속도제한
            // Debug.Log(string.Format("BallVelocity:{0}", rigid.velocity));
        }
    }

    private void CanvasFollow()
    {
       
        Vector3 delta = transform.position - startPos;  // 현재 공의 위치와 이전 공의 위치의 사잇값
        
        Vector3 pos = sizeCanvas.position; // 현재 캔버스 위치
        pos.x += delta.x; //사잇값 만큼 더하기
        pos.z += delta.z; //사잇값 만큼 더하기
        pos.y += delta.y; // 위치 + 공과 캔버스의 높이
        
        sizeCanvas.position = pos;
        startPos = transform.position; // 반복
    }

    private void ImageSizeUp()
    {
        Vector3 scale = sizeImage.transform.localScale;
        float radius = playerFirst.MyCol.radius;

        scale.x = radius;
        scale.y = radius;

        sizeImage.transform.localScale = scale;
    }

    private void SizeCheck()
    {

    }
}


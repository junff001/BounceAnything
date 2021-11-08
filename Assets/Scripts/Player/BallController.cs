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
    private Rigidbody rigid = null;
    public PlayerFirstObjScript playerFirst;

    [SerializeField]
    private LayerMask whatIsMovable;
    [SerializeField]
    private float moveSpeed = 2f;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    [SerializeField]
    private float rotationSpeed = 3f;
    public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }
    [SerializeField]
    private float jumpHeight = 10f;
    [SerializeField]
    private const float maxSpeed = 11f;

    private float totalTime = 0f;
    private bool canMove = true;
    private float yDelta;
    private float ColRadius;
    public Transform ballObjTrm; // 실제 공
    public RectTransform sizeCanvas;
    public RectTransform sizeNumCanvas;
    public Image sizeImage;
    private Vector3 startPos;
    private float radius;

    void Start()
    {
        gameManager = GameManager.Instance; 
        
        rigid = ballObjTrm.GetComponent<Rigidbody>();  
        playerInput = ballObjTrm.GetComponent<PlayerInput>(); 
        startPos = ballObjTrm.localPosition;
        radius = playerFirst.MyCol.radius;

        //gameManager.RespwnPlayer += () =>
        //{
        //    rigid. = Vector3.zero;
        //};
    }

    //private void OnEnable()
    //{
    //    startPos = ballObjTrm.position;
    //    radius = playerFirst.MyCol.radius;
    //}

    void FixedUpdate()
    {
        BallMove();
    }

    void Update()
    {
        ResetCanvasPos();

        startPos = ballObjTrm.localPosition; // 함수가 두번 실행 됨으로 반복 코드는 함수에서 따로 빼준다. 
        radius = playerFirst.MyCol.radius;
        
        ImageSizeUp(sizeCanvas);
        ImageSizeUp(sizeNumCanvas);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Movable"))
        {
            canMove = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Movable"))
        {
            canMove = false;
        }
    }

    private void BallMove()
    {
        if (canMove) { //Ball 이동관련
            if (playerInput.XMove > 0) {
                rigid.AddForce(Camera.main.gameObject.transform.forward * moveSpeed, ForceMode.Impulse);
            }
            else if (playerInput.XMove < 0) {
                rigid.AddForce(-Camera.main.gameObject.transform.forward * moveSpeed, ForceMode.Impulse);
            }
            if (playerInput.ZMove > 0) {
                rigid.AddForce(Camera.main.gameObject.transform.right * moveSpeed, ForceMode.Impulse);
            }
            else if (playerInput.ZMove < 0) {
                rigid.AddForce(-Camera.main.gameObject.transform.right * moveSpeed, ForceMode.Impulse);
            }
            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, maxSpeed); //공 속도제한
            // Debug.Log(string.Format("BallVelocity:{0}", rigid.velocity));
        }
    }

    private void CanvasFollow(RectTransform canvas) // 사이즈 이미지 함수
    {
        Vector3 delta = ballObjTrm.localPosition - startPos;  // 현재 공의 위치 - 이전 공의 위치 (두 점 사이의 거리)
                                       // 월드 스페이스 - 월드 스페이스
        float colDelta = playerFirst.MyCol.radius - radius;

        Vector3 pos = canvas.position; // 현재 캔버스 위치
        pos.x += delta.x; //사잇값 만큼 더하기
        pos.z += delta.z; //사잇값 만큼 더하기
        pos.y += delta.y - colDelta; // 위치 + 공과 캔버스의 높이

        canvas.position  = pos;
    }

    private void ImageSizeUp(RectTransform image)
    {
        Vector3 scale = image.transform.localScale;
        float radius = playerFirst.MyCol.radius;

        scale.x = radius;
        scale.y = radius;

        image.transform.localScale = scale;
    }

    public void ResetCanvasPos()
    {
        CanvasFollow(sizeCanvas);
        CanvasFollow(sizeNumCanvas);
    }


    private void SizeCheck()
    {

    }
}


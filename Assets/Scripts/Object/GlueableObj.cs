using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueableObj : MonoBehaviour
{
    private GameManager gameManager = null;
    private Vector3 originPos = Vector3.zero;
    private Quaternion originRotate = Quaternion.identity;
    private Collider myCol = null;

    [Header("이 오브젝트를 플레이어가 먹을 때, 먹을 수 있는가를 체크할 때 사용되는 첫 크기")]
    [SerializeField]
    private float sizeOrigin = 0f;
    [Header("플레이어가 이 오브젝트를 먹었을 때 늘어나는 크기")]
    [SerializeField]
    private float sizePlus = 0f;
    public float SizePlus
    {
        get{return sizePlus;}
    }
    public float SizeOrigin
    {
        get { return sizeOrigin; }
        set { sizeOrigin = value; }
    }
    public bool socreUp = false;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        myCol = GetComponent<Collider>();

        transform.SetParent(gameManager.GlueableObjParent);

        originPos = transform.position;
        originRotate = transform.rotation;

        // size = myCol.bounds.extents.x * myCol.bounds.extents.y * myCol.bounds.extents.z;
    }
    void Start()
    {
        gameManager.RestartGame += () =>
        {
            transform.SetParent(gameManager.GlueableObjParent);

            socreUp = false;
            transform.position = originPos;
            transform.rotation = originRotate;
        };
    }
}
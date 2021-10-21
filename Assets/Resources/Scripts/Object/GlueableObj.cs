using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueableObj : MonoBehaviour
{
    private GameManager gameManager = null;
    private Vector3 originPos = Vector3.zero;
    private Quaternion originRotate = Quaternion.identity;
    private Collider myCol = null;

    [Header("이 오브젝트가 플레이어에게 붙을 수 있는지에 대한 기준이 되는 크기")]
    [SerializeField]
    private float size = 0f; // 이 오브젝트의 크기
    public float Size 
    {
        get{return size;}
        set{size = value;}
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

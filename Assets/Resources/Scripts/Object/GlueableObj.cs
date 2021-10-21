using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueableObj : MonoBehaviour
{
    private GameManager gameManager = null;
    private Vector3 originPos = Vector3.zero;
    private Quaternion originRotate = Quaternion.identity;
    private Collider myCol = null;

    [Header("이 오브젝트의 크기, 붙었을 때 늘어나는 크기")]
    [SerializeField]
    private float size = 0f;
    public float Size
    {
        get { return size; }
        set { size = value; }
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

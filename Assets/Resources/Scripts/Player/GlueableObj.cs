using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueableObj : MonoBehaviour
{
    private GameManager gameManager = null;
    private Vector3 originPos = Vector3.zero;
    private Quaternion originRotate = Quaternion.identity;
    private Collider myCol = null;

    public float size { get; private set; }
    public bool socreUp = false;

    private void Awake() 
    {
        gameManager = GameManager.Instance;
        myCol = GetComponent<Collider>();

        transform.SetParent(gameManager.GlueableObjParent);

        originPos = transform.position;
        originRotate = transform.rotation;
        
        size = myCol.bounds.extents.x * myCol.bounds.extents.y * myCol.bounds.extents.z;

        gameManager.RestartGame += () =>
        {
            transform.SetParent(gameManager.GlueableObjParent);

            socreUp = false;
            transform.position = originPos;
            transform.rotation = originRotate;
        }; 
    }
    void Start()
    {

    }
}

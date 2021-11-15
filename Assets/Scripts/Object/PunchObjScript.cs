using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchObjScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask whatIsHitable = new LayerMask();

    private GlueableObj glueableObj = null;

    [SerializeField]
    private GameObject shootObj = null;

    [SerializeField]
    private float shootPower = 100f;
    [SerializeField]
    private float shootRange = 5f;

    [SerializeField]
    private float radius = 1f;

    private bool isShoot = false;

    private float firstDistance = 0f;

    private void Awake()
    {
        glueableObj = GetComponent<GlueableObj>();
    }
    void Start()
    {
        firstDistance = Vector3.Distance(transform.position, shootObj.transform.position);

        glueableObj.OnGlue += () =>
        {
            shootObj.GetComponent<Collider>().enabled = false;
            enabled = false;
        };
    }

    void Update()
    {
        Shoot();
        ShootReset();
    }
    private bool CheckPlayer()
    {
        return Physics.Raycast(transform.position, transform.right, shootRange, whatIsHitable);
    }
    private void Shoot()
    {
        if (CheckPlayer() && !isShoot)
        {
            Rigidbody rigid = shootObj.GetComponent<Rigidbody>();

            rigid.AddForce(Vector3.right * shootPower, ForceMode.Impulse);

            isShoot = true;
        }
    }
    private void ShootReset()
    {
        if (isShoot)
        {
            float distance = Vector3.Distance(transform.position, shootObj.transform.position);

            if (distance <= firstDistance)
            {
                isShoot = false;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * shootRange);
    }
}

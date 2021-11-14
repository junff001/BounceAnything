using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchObjScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask whatIsHitable = new LayerMask();
    [SerializeField]
    private GameObject shootObj = null;

    [SerializeField]
    private float shootPower = 100f;
    [SerializeField]
    private float shootRange = 5f;

    [SerializeField]
    private float radius = 1f;

    private bool isShoot = false;

    void Start()
    {
        
    }

    void Update()
    {
        Shoot();
    }
    private bool CheckPlayer()
    {
        return Physics.Raycast(transform.position, transform.right, shootRange, whatIsHitable);
    }
    private void Shoot()
    {
        if(CheckPlayer() && !isShoot)
        {
            Rigidbody rigid = shootObj.GetComponent<Rigidbody>();

            rigid.AddForce(Vector3.right * shootPower, ForceMode.Impulse);

            isShoot = true;
        }
    }
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * shootRange);
    }
}

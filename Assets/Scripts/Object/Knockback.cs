using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : BallController
{
    [Header("넉백 관련 변수")]
    [SerializeField]
    private float power = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 point;
        
        if (collision.gameObject.CompareTag("Player")) {
            for (int i = 0; i < collision.contacts.Length; i++) {
                point = collision.contacts[i].point;
                collision.rigidbody.AddForce(point.normalized * power, ForceMode.Impulse);
            }
        }
    } 

    
}

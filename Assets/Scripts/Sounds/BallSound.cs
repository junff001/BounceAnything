using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSound : MonoBehaviour
{
    private AudioSource audioSource0;
    private AudioSource audioSource1;
    private Rigidbody rigid;
    private int child = 0;

    void Start()
    {
        audioSource0 = transform.GetChild(0).GetComponent<AudioSource>();
        audioSource1 = transform.GetChild(1).GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
        child = transform.childCount;
    }

    void Update()
    {
        Attach();
    }

    private void Attach() // 부착 효과음
    {
        if (child < transform.childCount) {
            audioSource0.Play();
        }
        child = transform.childCount;
    }

    private void OnCollisionEnter(Collision collision) // 충돌 효과음
    {
        int layerMask = LayerMask.NameToLayer("GROUND");

        if (rigid.velocity.magnitude > 2.5f) {
            if (collision.gameObject.layer != layerMask) {
                audioSource1.Play();
            }
        }
    }
}

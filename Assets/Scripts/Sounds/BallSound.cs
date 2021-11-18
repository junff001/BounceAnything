using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSound : MonoBehaviour
{
    private AudioSource audioSource0;
    private AudioSource audioSource1;
    private int child = 0;

    void Start()
    {
        audioSource0 = transform.GetChild(0).GetComponent<AudioSource>();
        audioSource1 = transform.GetChild(1).GetComponent<AudioSource>();
        child = transform.childCount;
    }

    void Update()
    {
        Attach();
    }

    private void Attach()
    {
        if (child < transform.childCount) {
            audioSource0.Play();
        }
        child = transform.childCount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        int layerMask = LayerMask.NameToLayer("GROUND");

        if (collision.gameObject.layer != layerMask) {
            audioSource1.Play();
        }  
    }
}
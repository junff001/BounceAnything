using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachSound : MonoBehaviour
{
    private AudioSource audioSource;
    private int child = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        child = transform.childCount;
    }

    void Update()
    {
        if (child < transform.childCount) {
            audioSource.Play();
            child = transform.childCount;
        }
    }
}

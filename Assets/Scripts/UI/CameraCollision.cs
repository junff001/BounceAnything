using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    private GameManager gameManager = null;

    [SerializeField]
    private LayerMask whatIsDisappear;
    [SerializeField]
    private float offset = 2f;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void FixedUpdate()
    {
        if (gameManager.PlayerFirstObjScript != null)
        {
            Ray ray = new Ray();
            bool hit = false;

            Vector3 palyerPos = gameManager.PlayerFirstObjScript.transform.position;

            Vector3[] origins = new Vector3[]
            {
                palyerPos,
                palyerPos + Vector3.left * offset,
                palyerPos + Vector3.right * offset,
                palyerPos + Vector3.up * offset,
                palyerPos + Vector3.down * offset,
                palyerPos + Vector3.forward * offset,
                palyerPos + Vector3.back * offset
            };

            foreach (Vector3 origin in origins)
            {
                ray.origin = origin;
                ray.direction = transform.position - origin;

                Debug.DrawRay(ray.origin, ray.direction * Vector3.Distance(ray.origin, ray.direction / offset), Color.blue, 0f);

                if(RayCheck(ray))
                {
                    hit = true;
                }
            }

            ScreenManager.Instance.dontShowWall = hit;
        }
    }

    private bool RayCheck(Ray ray)
    {
        float distance = Vector3.Distance(ray.origin, ray.direction) / offset;

        return Physics.Raycast(ray, distance, whatIsDisappear);
    }
}

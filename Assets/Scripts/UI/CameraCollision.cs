using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    private GameManager gameManager = null;

    [SerializeField]
    private LayerMask whatIsDisappear;

    private List<RaycastHit> pasteHits = new List<RaycastHit>();
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
            List<RaycastHit> hits = new List<RaycastHit>();

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

                hits = hits.SumList(RayCheck(ray));
            }

            hits = hits.Distinct().ToList();

            foreach(RaycastHit hit in pasteHits)
            {
                bool isIn = false;

                foreach(RaycastHit hit2 in hits)
                {
                    if(hit.collider.gameObject == hit2.collider.gameObject)
                    {
                        isIn = true;

                        break;
                    }
                }

                if(!isIn)
                {
                    hit.collider.gameObject.layer = hit.collider.gameObject.layer = LayerMask.GetMask("WALL").GetLayer<LayerMask>();
                }
            }

            foreach(RaycastHit hit in hits)
            {
                hit.collider.gameObject.layer = LayerMask.GetMask("DISAPEARWALL").GetLayer<LayerMask>();
            }

            pasteHits = hits;
            
            ScreenManager.Instance.dontShowWall = hits.Count > 0;
        }
    }

    private List<RaycastHit> RayCheck(Ray ray)
    {
        float distance = Vector3.Distance(ray.origin, ray.direction) / offset;

        return Physics.RaycastAll(ray, distance, whatIsDisappear).ToList<RaycastHit>();
    }
}

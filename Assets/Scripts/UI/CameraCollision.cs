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

    private List<RaycastHit> pasteHits = new List<RaycastHit>();

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

            HitsCheck(hits);//TODO: 여러방향으로의 Raycast 체크 후 List<RaycastHit>들을 모두 SumList를 통해 합치고 중복제거 후 HitCheck함수 실행

            foreach (RaycastHit item in hits)
            {
                item.collider.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }

            pasteHits = hits;
        }
    }

    private List<RaycastHit> RayCheck(Ray ray)
    {
        List<RaycastHit> hits;
        float distance = Vector3.Distance(ray.origin, ray.direction) / offset;

        hits = Physics.RaycastAll(ray, distance, whatIsDisappear).ToList<RaycastHit>();

        return hits;
    }

    private void HitsCheck(List<RaycastHit> hits) // PasteHits는 이전에 사라졌던 오브젝트들임, pasteHits엔 있고 현재 hits에 없다면, 그놈은 다시 나타나야함. 다시 나타나는 것을 체크해줌
    {
        if (hits != pasteHits)
        {
            foreach (RaycastHit value in pasteHits)
            {
                bool isSame = false;

                foreach (RaycastHit value2 in hits)
                {
                    if (value.collider.gameObject == value2.collider.gameObject)
                    {
                        isSame = true;

                        break;
                    }
                }

                if (!isSame)
                {
                    value.collider.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }
}

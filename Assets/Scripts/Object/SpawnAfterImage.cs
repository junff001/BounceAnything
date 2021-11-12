using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAfterImage : MonoBehaviour
{
    private PoolManager poolManager = null;
    [SerializeField]
    private GameObject afterImage = null;

    private void Awake()
    {
        poolManager = PoolManager.Instance;
    }

    public void Spawn(Vector3 spawnPos, float radius, Quaternion angle)
    {
        if (poolManager.AfterImages.Count > 0)
        {
            GameObject a = poolManager.AfterImages.Dequeue().gameObject;

            a.SetActive(true);
            a.transform.position = spawnPos;
            a.transform.localScale = new Vector3(2 * radius, 2 * radius, 2 * radius);
            a.transform.rotation = angle;
        }
        else
        {
            GameObject a = Instantiate(afterImage, poolManager.transform);

            a.transform.position = spawnPos;
            a.transform.localScale = new Vector3(2 * radius, 2 * radius, 2 * radius);
            a.transform.rotation = angle;
        }

    }
}

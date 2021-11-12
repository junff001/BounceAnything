using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private GameManager gameManager = null;
    private PoolManager poolManager = null;

    private MeshRenderer meshRenderer = null;
    private Material material = null;

    [SerializeField]
    private float originAlpha = 0.5f;
    [SerializeField]
    private float fadeOutTime = 3f;
    private float fadeOutTimer = 0f;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        poolManager = PoolManager.Instance;

        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
    }
    void OnEnable()
    {
        fadeOutTimer = fadeOutTime;
    }
    void Update()
    {
        //transform.localScale = gameManager.CurrentPlayerObj.GetComponent<SphereCollider>().
        SetAlpha();
        DespawnCheck();
    }

    private void SetAlpha()
    {
        material.color = new Vector4(1f, 1f, 1f, originAlpha * (fadeOutTimer / fadeOutTime));
    }
    private void DespawnCheck()
    {
        fadeOutTimer -= Time.deltaTime;

        if(fadeOutTimer < 0)
        {
            Despawn();
        }
    }
    private void Despawn()
    {
        poolManager.AfterImages.Enqueue(this);
        gameObject.SetActive(false);
    }
}

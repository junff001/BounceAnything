using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScoreTextScript : MonoBehaviour
{
    private Text text = null;

    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private float fadeOutTime = 1f;
    private float fadeOutTimer = 0f;

    private Vector2 currentPosition = Vector2.zero;

    private void Start() 
    {
        text = GetComponent<Text>();
    }
    private void OnEnable() 
    {
        fadeOutTimer = fadeOutTime;
    }
    void Update()
    {
        currentPosition = transform.position;

        if(fadeOutTimer > 0f)
        {
            fadeOutTimer -= Time.deltaTime;

            text.color = new Vector4(1f, 1f, 1f, Mathf.Lerp(0, 1, fadeOutTime / fadeOutTimer));

            currentPosition = Vector2.MoveTowards(currentPosition, currentPosition + Vector2.up, moveSpeed * Time.deltaTime);
        }
        else
        {
            PoolManager.Instance.TextObjQueue.Enqueue(gameObject);

            gameObject.SetActive(false);
        }    

        transform.position = currentPosition;
    }
}

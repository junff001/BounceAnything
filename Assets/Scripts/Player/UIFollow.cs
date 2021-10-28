using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//BallSize 캔버스의 위치를 잡아주는 스크립트

public class UIFollow : MonoBehaviour  
{
    public Transform targetObject;
    public Image sizeImage;
    public PlayerFirstObjScript playerFirst;
    public Vector3 before;

    private float yDelta;
    private float ColRadius;

    void Start()
    {
        before = targetObject.position;
        yDelta = targetObject.position.y - transform.position.y;
        ColRadius = playerFirst.MyCol.radius;
    }

    void Update()
    {
        Follow();
        //SizeUp();
    }

    private void Follow() // 캔버스가 공을 기준으로 바닥에 붙어다님
    {
        Vector3 delta = targetObject.position - before;

        Vector3 pos = transform.position;
        pos.x += delta.x;
        pos.z += delta.z;
        pos.y = delta.y + yDelta;

        transform.position = pos;
        before = targetObject.position;
    }

    private void SizeUp() // 캔버스의 이미지가 컬라이더 반지름만큼 커짐
    {
        sizeImage.transform.localScale = new Vector3(ColRadius, ColRadius, ColRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    [SerializeField]
    private Transform[] patrolPoints;
    private NavMeshAgent agent;

    [SerializeField]
    private float minDistance = 1f;
    private int pointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) {
            agent.autoBraking = false;  // 목적지에 가까워질 때 자동 브레이크 Off
            NextPoint();
        }
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= minDistance) { // 경로 배정 준비가 되어있고 && 일정 거리가 유지되었다면
            NextPoint();
        }
    }  

    private void NextPoint()
    {
        agent.destination = patrolPoints[pointIndex].position;
        pointIndex = (pointIndex + 1) % patrolPoints.Length; // 나머지 값을 변수에 할당함으로써 사이클 형성
    }
}

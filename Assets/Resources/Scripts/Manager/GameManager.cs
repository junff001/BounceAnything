using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject temp = new GameObject("GameManager");
                    instance = temp.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }

    [SerializeField]
    private LayerMask whatIsPlayerFirstObj;
    public LayerMask WhatisPlayerFirstObj
    {
        get { return whatIsPlayerFirstObj; }
    }

    private PlayerFirstObjScript playerFirstObjScript = null;
    public PlayerFirstObjScript PlayerFirstObjScript
    {
        get
        {
            return playerFirstObjScript;
        }
        set
        {
            playerFirstObjScript = value;
        }
    }
    void Start()
    {

    }
    void Update()
    {

    }
}

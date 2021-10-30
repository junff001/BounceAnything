using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    private static ScreenManager instance;
    public static ScreenManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScreenManager>();
            }

            return instance;
        }
    }
    
    private Sprite screenSprite = null;
    [SerializeField]
    private Image screen = null;

    public bool dontShowWall { get; set; }

    private void Start()
    {
        screenSprite = Resources.Load<Sprite>("Image/Screen/Screen_Base");
    }

    private void Update()
    {
        if (dontShowWall)
        {
            screen.sprite = screenSprite;
        }
        else
        {
            screen.sprite = null;
        }
    }
}

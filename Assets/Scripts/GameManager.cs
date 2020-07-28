using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager _instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("Game manager is null");
            }
            return instance;
        }
    }

    public bool hasKey { get; set; }
    public void Awake()
    {
        instance = this;
    }
}

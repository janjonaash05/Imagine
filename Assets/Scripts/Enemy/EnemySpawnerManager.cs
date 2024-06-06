using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private float repeatDelay;
    [SerializeField] private float startDelay;
    public float RepeatDelay => repeatDelay;
    public float StartDelay => startDelay;

    public static EnemySpawnerManager Instance { get; private set; }



    private void Awake()
    {
        Assert.IsTrue(repeatDelay > 0, "repeat delay must be positive");
        Assert.IsTrue(startDelay > 0, "start delay must be positive");


        if (Instance == null) { Instance = this;  }

        
    }
}

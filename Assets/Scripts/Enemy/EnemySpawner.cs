using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    private System.Random rand;
    [SerializeField] private float delay;
    [SerializeField] private float upOffset;
    private void Awake()
    {
        Assert.IsTrue(prefabs.Length > 0 && upOffset >= 0 && delay > 0);
        rand = new();

        InvokeRepeating(nameof(Spawn), (float)rand.NextDouble() * delay, delay);
    }



    private void Spawn()
    {
        Instantiate(prefabs[rand.Next(prefabs.Length)], transform.position + upOffset * Vector3.up, transform.rotation);



    }









}

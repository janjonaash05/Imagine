using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;



/// <summary>
/// Manages prefab spawning with an interval.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private float upOffset;
    private System.Random rand;
    private void Awake()
    {
 
        Assert.IsTrue(prefabs.Length > 0, "prefabs length must be positive");
        rand = new();

        StartCoroutine(ReadyCheck());
       
    }

    private IEnumerator ReadyCheck()
    {
        while (EnemySpawnerManager.Instance == null) yield return null;
        StartSpawning();

    }



    private void StartSpawning() 
    {
        InvokeRepeating(nameof(Spawn), EnemySpawnerManager.Instance.StartDelay, EnemySpawnerManager.Instance.RepeatDelay);
    }



    private void Spawn()
    {
        Instantiate(prefabs[rand.Next(prefabs.Length)], transform.position + upOffset * Vector3.up, transform.rotation);


    }









}

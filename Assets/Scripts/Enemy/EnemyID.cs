using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


/// <summary>
/// Holds the enemy's type (enum).
/// </summary>
public class EnemyID : MonoBehaviour
{
    

    [SerializeField] private string name;

    public string Name => name;


    private void Awake()
    {
        Assert.IsTrue(name != null && !name.Equals(string.Empty), "name is not null or empty");
    }

}

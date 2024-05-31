using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyID : MonoBehaviour
{
    public enum EnemyType {Red,Green,Blue }

    [SerializeField] private EnemyType type;

    
    public EnemyType Type => type;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyID : MonoBehaviour
{
    public enum EnemyType {RED,GREEN,BLUE }

    [SerializeField] EnemyType type;


    public EnemyType Type => type;

}

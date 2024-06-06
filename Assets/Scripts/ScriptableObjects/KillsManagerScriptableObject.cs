using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName=nameof(KillsManagerScriptableObject),menuName = "ScriptableObjects/Kills Manager")]
public class KillsManagerScriptableObject : ScriptableObject
{

    public event Action<string> OnNamedEnemyKilled;

    public void KillNamedEnemy(string name) => OnNamedEnemyKilled?.Invoke(name);

}

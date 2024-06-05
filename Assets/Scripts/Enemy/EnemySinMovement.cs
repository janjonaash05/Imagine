using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


/// <summary>
/// Overrides the base AfterMoveAdjust to a vertical sine movement.
/// </summary>
public class EnemySinMovement : EnemyMovement
{
    
    [SerializeField] private float frequency;
    [SerializeField] private float waveMultiplier;
    protected override Vector3 AfterMoveAdjust()
    {
        return new(rb.position.x, (Mathf.Sin(Time.fixedTime * frequency) * waveMultiplier * Time.fixedDeltaTime) + startY, rb.position.z);
    }


    protected new void Awake()
    {
        base.Awake();

        Assert.IsTrue(frequency > 0 && waveMultiplier >0);
    }
}

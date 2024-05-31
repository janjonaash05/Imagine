using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySinMovement : EnemyMovement
{
    // Start is called before the first frame update
    [SerializeField] private float frequency;
    [SerializeField] private float waveMultiplier;
    protected override Vector3 AfterMoveAdjust()
    {
        return new(rb.position.x, (Mathf.Sin(Time.fixedTime) * speed * waveMultiplier * Time.fixedDeltaTime) + startY, rb.position.z);
    }
}

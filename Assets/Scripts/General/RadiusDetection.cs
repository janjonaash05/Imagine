using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


/// <summary>
/// Manages catching colliders on a certain layer and invoking events based on what it found or not found.
/// </summary>
public class RadiusDetection : MonoBehaviour
{
    public event Action<GameObject> OnObjectCaught;
    public event Action OnObjectLost;

    

    [SerializeField] private LayerMask includeMask;

    

    [SerializeField] private float radius;

    private void Awake()
    {
        Assert.IsTrue(radius > 0);
        results = new Collider[1];
    }



    private Collider[] results;
    private void FixedUpdate()
    {
         
        if (Physics.OverlapSphereNonAlloc(transform.position, radius, results, includeMask, QueryTriggerInteraction.Ignore) == 0)
        {
            OnObjectLost?.Invoke();
        }

        else if (results[0].gameObject != null) 
        {
            OnObjectCaught?.Invoke(results[0].gameObject);
        }

       
    }


   
}

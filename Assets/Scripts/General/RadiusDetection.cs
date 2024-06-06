using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;


/// <summary>
/// Manages catching colliders on a certain layer and invoking events based on what it found or not found.
/// </summary>
public class RadiusDetection : MonoBehaviour
{

    


    [SerializeField] private LayerMask includeMask;
    [SerializeField] private float radius;

    public event Action<GameObject> OnObjectCaught;
    public event Action OnObjectLost;

    private void Awake()
    {
        Assert.IsTrue(radius > 0);
    }



    private void FixedUpdate()
    {

       var results = Physics.OverlapSphere(transform.position, radius, includeMask, QueryTriggerInteraction.Ignore);

        if(results.Length == 0 ) 
        {
            OnObjectLost?.Invoke();
            return;
        }

        results = results.OrderBy(x => Vector3.SqrMagnitude(x.transform.position - transform.position)).ToArray();

        if (results[0].gameObject != null)
        {
            OnObjectCaught?.Invoke(results[0].gameObject);
        }


    }



}

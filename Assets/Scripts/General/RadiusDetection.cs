using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusDetection : MonoBehaviour
{
    public event Action<GameObject> OnObjectCaught;
    public event Action OnObjectLost;

    

    [SerializeField] private LayerMask includeMask;

    

    [SerializeField] private float radius;


  



    private void FixedUpdate()
    {
        var result = Physics.OverlapSphere(transform.position, radius, includeMask,QueryTriggerInteraction.Ignore);
        if (result.Length == 0)
        {
            OnObjectLost?.Invoke();
        }

        else if (result[0].gameObject != null) 
        {
            OnObjectCaught?.Invoke(result[0].gameObject);
        }

       
    }


   
}
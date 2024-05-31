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


    private void OnTriggerEnter(Collider other)
    {
        /*
        if (((1 << other.gameObject.layer) & includeMask) == 0) return;




        inTrigger = true;
        otherCollider = other;

        OnObjectCaught?.Invoke(other.gameObject);
        */
    }


    private void OnTriggerExit(Collider other)
    {
        /*
        if (((1 << other.gameObject.layer) & includeMask) == 0) return;




        inTrigger = false;


        OnObjectLost?.Invoke();
        */
    }



    private void FixedUpdate()
    {
        var result = Physics.OverlapSphere(transform.position, radius, includeMask,QueryTriggerInteraction.Ignore);
        if (result.Length == 0)
        {
            OnObjectLost?.Invoke();
        }
        else OnObjectCaught?.Invoke(result[0].gameObject);
    }


   
}

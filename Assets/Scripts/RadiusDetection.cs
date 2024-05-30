using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusDetection : MonoBehaviour
{
    public event Action<GameObject> OnObjectCaught;
    public event Action OnObjectLost;

    [SerializeField] LayerMask includeMask; 

    private SphereCollider coll;

    [SerializeField] float radius;

    private void Start()
    {
        coll = GetComponent<SphereCollider>();
        coll.radius = radius;

    }



    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & includeMask) == 0) return;

            Debug.Log("CAUGHT");
        OnObjectCaught?.Invoke(other.gameObject);
    }


    private void OnTriggerExit(Collider other)
    {

        if (((1 << other.gameObject.layer) & includeMask) == 0) return;

        Debug.Log("LOST");
        OnObjectLost?.Invoke();
    }
}

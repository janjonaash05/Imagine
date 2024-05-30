using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public event Action<GameObject> OnObjectCaught;


    private SphereCollider coll;

    [SerializeField] float radius;

    private void Start()
    {
        coll = GetComponent<SphereCollider>();
        coll.radius = radius;

    }



    private void OnTriggerEnter(Collider other)
    {
        OnObjectCaught?.Invoke(other.gameObject);
    }
}

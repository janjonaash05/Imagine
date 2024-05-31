using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class HealthFall : MonoBehaviour
{

    private bool grounded = false;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundingDistance;

    private Rigidbody rb;

    private void Awake()
    {
        Assert.IsTrue(groundingDistance > 0);
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        if(grounded) return;


        if (Physics.Raycast(transform.position, Vector3.down, groundingDistance, groundMask))
        {
          
            grounded = true;
            rb.isKinematic = true;
        }
    }
}

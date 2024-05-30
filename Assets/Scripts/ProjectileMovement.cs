using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public enum ProjectileType { FRIENDLY, ENEMY }
    [SerializeField] private float speed;
    [SerializeField] private ProjectileType projectileType;
    private Rigidbody rb;
    private new Collider collider;


   

    [SerializeField]
    private LayerMask ignoreMask;

    [SerializeField] 
    private LayerMask hitMask;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    private Vector3 dir;

    public void EnableCollision() => collider.enabled = true;
    public void SetDirection(Vector3 dir) => this.dir = dir;

    void FixedUpdate()
    {


        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * dir);

    }


    /*
     *  if(((1<<other.gameObject.layer) & includeLayers) != 0)
        {
       //It matched one
        }

        if(((1<<other.gameObject.layer) & ignoreLayers) == 0)
        {
        //It wasn't in an ignore layer
        }
     * 
     * 
     * 
     */

    private void OnTriggerEnter(Collider collider)
    {

        if ((1 << collider.gameObject.layer & ignoreMask) != 0) return;

        if ((1 << collider.gameObject.layer & hitMask) != 0)
        {
            collider.GetComponent<Health>().Damage();
        }

        Destroy(gameObject);
    }
}

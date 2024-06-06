using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


/// <summary>
/// Manages the movement, collision and death of the projectile.
/// </summary>
public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float projectileDeathTimeout;
    private float speed;



    private Rigidbody rb;
    private new Collider collider;

    private int damage;


    [SerializeField]
    private LayerMask ignoreMask;

    [SerializeField]
    private LayerMask hitMask;

    

    private ParticleSystem deathPS;
    private Vector3 dir;

    private Material projectileMat, projectilePSMat, projectilePSTrailMat;
    private Renderer rend;
    public void SetDirection(Vector3 dir) => this.dir = dir;





    /// <summary>
    /// Sets the damage multiplier and adds force to the RigidBody in a previously set direction.
    /// </summary>
    /// <param name="damage"></param>
    public void Launch(int speed, int damage,List<Collider> toIgnoreColliders)
    {
        foreach(var item in toIgnoreColliders) 
        {
            Physics.IgnoreCollision(collider, item,true);
        }


        collider.enabled = true;

        this.speed = speed;
        this.damage = damage;

        Destroy(gameObject, projectileDeathTimeout);

    }

    private void Awake()
    {

        Assert.IsTrue(projectileDeathTimeout > 0, "projectile death timeout is positive");

        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        collider.enabled = false;



        AssignMaterials();
    }


    private void AssignMaterials() 
    {
        deathPS = transform.GetChild(0).GetComponent<ParticleSystem>();
        var deathPSrend = deathPS.GetComponent<ParticleSystemRenderer>();
        rend = GetComponent<Renderer>();




        projectileMat = rend.material;
  
        projectilePSMat = deathPSrend.material;
        projectilePSTrailMat = deathPSrend.trailMaterial;



        projectilePSMat = projectileMat;
        projectilePSTrailMat = projectileMat;

    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (speed * Time.fixedDeltaTime * dir));
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider == null) return;

        if ((1 << collider.gameObject.layer & ignoreMask) != 0) return;

        if ((1 << collider.gameObject.layer & hitMask) != 0)
        {
            collider.GetComponent<Health>().Damage(damage);
        }

        Destroy(this.collider);
        Destroy(GetComponent<Renderer>());
        rb.isKinematic = true;
        StartCoroutine(PlayDeathPS());
    }


    private void OnDestroy()
    {
        Destroy(projectileMat);
        Destroy(projectilePSMat);
        Destroy(projectilePSTrailMat);
    }

    private IEnumerator PlayDeathPS()
    {
        var emission = deathPS.emission;
        emission.enabled = true;

        deathPS.Play();

        yield return new WaitForSeconds(deathPS.main.duration);
        Destroy(gameObject);

    }



}

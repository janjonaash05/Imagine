using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


/// <summary>
/// Manages the movement, collision and death of the projectile.
/// </summary>
public class ProjectileMovement : MonoBehaviour
{

    [SerializeField] private float speed;

    private Rigidbody rb;
    private new Collider collider;

    private int damage;


    [SerializeField]
    private LayerMask ignoreMask;

    [SerializeField]
    private LayerMask hitMask;

    private ParticleSystem deathPS;


    private void Awake()
    {
        Assert.IsTrue(speed > 0);


        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        collider.enabled = false;


        deathPS = transform.GetChild(0).GetComponent<ParticleSystem>();
        var deathPSrend = deathPS.GetComponent<ParticleSystemRenderer>();
        var rend = GetComponent<Renderer>();

        deathPSrend.material = rend.material;
        deathPSrend.trailMaterial = rend.material;
    }

    private Vector3 dir;

    public void EnableCollision() => collider.enabled = true;
    public void SetDirection(Vector3 dir) => this.dir = dir;



    /// <summary>
    /// Sets the damage multiplier and adds force to the RigidBody in a previously set direction.
    /// </summary>
    /// <param name="damage"></param>
    public void Launch(int damage)
    {
        this.damage = damage;
        rb.AddForce(speed * Time.fixedDeltaTime * dir);

    }



    private void OnTriggerEnter(Collider collider)
    {

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


    private IEnumerator PlayDeathPS()
    {
        var emission = deathPS.emission;
        emission.enabled = true;

        deathPS.Play();



        yield return new WaitForSeconds(deathPS.main.duration);
        Destroy(gameObject);



    }



}

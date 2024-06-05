using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;


/// <summary>
/// Manages the automatic shooting when in the vicinity of a target
/// </summary>
public class Shooting : MonoBehaviour
{
    protected RadiusDetection radiusDetection;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float projectileDeathTimeout;
    [SerializeField] protected float fireRate;
    [SerializeField] protected int damageMult;


    private Coroutine autoShootingCoroutine;


    protected bool ableToAutoShoot = true;
    protected bool manuallyShooting = false;

    protected WaitForSeconds delay;


    private List<Collider> colliders;

    protected void Awake()
    {
        Assert.IsNotNull(projectilePrefab);
        Assert.IsTrue(projectileDeathTimeout > 0);
        Assert.IsTrue(fireRate > 0 && damageMult > 0);

        radiusDetection = GetComponent<RadiusDetection>();
        delay = new(1f / fireRate);

        colliders = GetComponents<Collider>().ToList();

    }



    protected void Start()
    {


        radiusDetection.OnObjectCaught += StartAutoShooting;
        radiusDetection.OnObjectLost += StopAutoShooting;
    }


    protected void OnDestroy()
    {
        radiusDetection.OnObjectCaught -= StartAutoShooting;
        radiusDetection.OnObjectLost -= StopAutoShooting;
    }


    protected void StopAutoShooting()
    {
        if (autoShootingCoroutine != null) StopCoroutine(autoShootingCoroutine);

        ableToAutoShoot = true;
    }


    protected void StartAutoShooting(GameObject targetObject)
    {
        if (!ableToAutoShoot || manuallyShooting) return;

        autoShootingCoroutine = StartCoroutine(StartShootingEnumerator(targetObject));
    }


    protected IEnumerator StartShootingEnumerator(GameObject g)
    {
        ableToAutoShoot = false;

        while (true)
        {
            if (g == null) { ableToAutoShoot = true; yield break; }

            ShootFromCenter(g.transform.position);


            yield return delay;


        }


    }




    protected void ShootFromCenter(Vector3 target)
    {
        SummonBullet(transform.position, target);
    }

    protected void Shoot(Vector3 origin, Vector3 target) => SummonBullet(origin, target);



    /// <summary>
    /// Instantiates the projectile prefab at an origin point, makes it ignore this gameObject's collider(s), calculates the direction, launches it with a damageMultiplier.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    private void SummonBullet(Vector3 origin, Vector3 target)
    {
        var projectile = Instantiate(projectilePrefab, origin, transform.rotation);



        foreach (var collider in colliders)
        {
            if (collider != null)
                Physics.IgnoreCollision(projectile.GetComponent<Collider>(), collider, true);
        }

        var movement = projectile.GetComponent<ProjectileMovement>();
        movement.EnableCollision();
        movement.SetDirection((target - origin).normalized);
        movement.Launch(damageMult);

        Destroy(projectile, projectileDeathTimeout);



    }



}

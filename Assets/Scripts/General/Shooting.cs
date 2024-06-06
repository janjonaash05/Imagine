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
   // [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected ProjectileMovement movementPrefab;

    [SerializeField] protected float fireRate;
    [SerializeField] protected int damageMult;
    [SerializeField] protected int projectileSpeed;
    [SerializeField] protected float projectileSize;


    private Coroutine autoShootingCoroutine;


    protected bool ableToAutoShoot = true;
    protected bool manuallyShooting = false;

    protected WaitForSeconds delay;


    private List<Collider> colliders;


    private Vector3 projectileScale;



    protected void Awake()
    {
        Assert.IsNotNull(movementPrefab, "movement prefab is not null");
        
        Assert.IsTrue(fireRate > 0, "fire rate is positive");
        Assert.IsTrue(damageMult > 0, "damage multiplier is positive");
        Assert.IsTrue(projectileSize > 0, "projectile size is positive");


        radiusDetection = GetComponent<RadiusDetection>();
        delay = new(1f / fireRate);

        colliders = GetComponents<Collider>().ToList();


        projectileScale = projectileSize * Vector3.one;

        

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


        var collider = g.GetComponent<Collider>();
        ableToAutoShoot = false;

        while (true)
        {
            if (collider == null) { ableToAutoShoot = true; yield break; }

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
    /// Instantiates the projectile movement prefab at an origin point, sets the direction, launches it with speed, damage multiplier and colliders to ignore.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    private void SummonBullet(Vector3 origin, Vector3 target)
    {



        var movement = Instantiate(movementPrefab, origin, transform.rotation);
        movement.transform.localScale = projectileScale;
        movement.SetDirection((target - origin).normalized);
        movement.Launch(projectileSpeed, damageMult, colliders);

        



    }



}

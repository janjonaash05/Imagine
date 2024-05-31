using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class Shooting : MonoBehaviour
{
    protected RadiusDetection radiusDetection;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float projectileDeathTimeout;
    [SerializeField] protected float fireRate;



    private Coroutine autoShootingCoroutine;


    protected bool ableToAutoShoot = true;
    protected bool manuallyShooting = false;

    protected WaitForSeconds delay;

    private void Awake()
    {

        radiusDetection = GetComponent<RadiusDetection>();
        delay = new(1f/fireRate);
        Assert.IsNotNull(projectilePrefab);
        Assert.IsTrue(projectileDeathTimeout > 0);
        Assert.IsTrue(fireRate > 0);
    }



    protected virtual void Start()
    {
        
        
        radiusDetection.OnObjectCaught += StartAutoShooting;
        radiusDetection.OnObjectLost += StopAutoShooting;
    }


    protected virtual void OnDestroy()
    {
        radiusDetection.OnObjectCaught -= StartAutoShooting;
        radiusDetection.OnObjectLost -= StopAutoShooting;
    }


    protected void StopAutoShooting()
    {
        if(autoShootingCoroutine != null) StopCoroutine(autoShootingCoroutine); 
        
        ableToAutoShoot = true;
    }


    protected void StartAutoShooting(GameObject targetObject)
    {
        if (!ableToAutoShoot || manuallyShooting) return;

      autoShootingCoroutine =  StartCoroutine(StartShootingEnumerator(targetObject));
    }


    protected IEnumerator StartShootingEnumerator(GameObject g)
    {
        ableToAutoShoot = false;

        while (true)
        {
            if (g == null) {  ableToAutoShoot = true; yield break; }

            ShootFromCenter(g.transform.position);


            yield return delay;


        }


    }




    protected void ShootFromCenter(Vector3 target)
    {
        SummonBullet(transform.position, target);
    }

    protected void Shoot(Vector3 origin, Vector3 target) => SummonBullet(origin, target);



    private void SummonBullet(Vector3 origin,Vector3 target) 
    {
        var projectile = Instantiate(projectilePrefab, origin, transform.rotation);

        if (GetComponent<Collider>() != null) Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>(), true);


        var movement = projectile.GetComponent<ProjectileMovement>();
        movement.EnableCollision();
        movement.SetDirection((target - origin).normalized);
        movement.Launch();

        Destroy(projectile, projectileDeathTimeout);



    }



}

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


    private void OnDestroy()
    {
        radiusDetection.OnObjectCaught -= StartAutoShooting;
        radiusDetection.OnObjectLost -= StopAutoShooting;
    }


    protected void StopAutoShooting()
    {
        if(autoShootingCoroutine != null)
        StopCoroutine(autoShootingCoroutine); 
        
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


        //var delay = new WaitForSeconds(1f / fireRate);
        while (true)
        {
            if (g == null) {  ableToAutoShoot = true; yield break; }

            ShootAt(g.transform.position);


            yield return delay;


        }


    }


    protected void ShootAt(Vector3 target)
    {
        var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        if (GetComponent<Collider>() != null) Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>(), true);


        var movement = projectile.GetComponent<ProjectileMovement>();
        movement.EnableCollision();
        movement.SetDirection((target - transform.position).normalized);
        movement.Launch();

        Destroy(projectile, projectileDeathTimeout);



    }



}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private RadiusDetection radiusDetection;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] float projectileDeathTimeout;
    [SerializeField] float fireRate;

    

    private bool ableToAutoShoot = true;
    private Collider collider;



    private void Start()
    {
        collider = GetComponent<Collider>();
        radiusDetection.OnObjectCaught += (g) => StartCoroutine(StartShooting(g));
        radiusDetection.OnObjectLost += () => { StopAllCoroutines(); ableToAutoShoot = true; };
    }

    private IEnumerator StartShooting(GameObject g) 
   {
        if (!ableToAutoShoot) yield break;

        ableToAutoShoot = false;

        WaitForSeconds delay = new(1/fireRate);
        while (true)
        {
            Debug.LogError("shooting");
            ShootAt(g);
            yield return delay;
        

        }
    
    
   }


    private void ShootAt(GameObject g) 
    {
        var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), collider, true);


        Debug.Log("shooting "+  (TryGetComponent<EnemyID>(out EnemyID component) ? component.Type : "" )   );
        var movement = projectile.GetComponent<ProjectileMovement>();
        movement.EnableCollision();
        movement.SetDirection((g.transform.position - transform.position).normalized);

       

    }


  
}

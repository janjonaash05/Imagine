using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class PlayerShooting : Shooting
{


    private PlayerController controller;

    private Coroutine manuallyShootingCoroutine;

    [SerializeField] float upwardsTilt;


    protected override void Start()
    {
        base.Start();
        controller = PlayerController.Instance;

        controller.OnPlayerShootingStart += StartManuallyShooting;
        controller.OnPlayerShootingEnd += StopManuallyShooting;
        


        
    }

    private void OnDestroy()
    {
        controller.OnPlayerShootingStart -= StartManuallyShooting;

        controller.OnPlayerShootingEnd -= StopManuallyShooting;
    }


    private void StartManuallyShooting()
    {

        manuallyShooting = true;
        manuallyShootingCoroutine = StartCoroutine(ShootingEnumerator());

    }

    private void StopManuallyShooting() 
    {

        manuallyShooting = false;
        if (manuallyShootingCoroutine != null)
            StopCoroutine(manuallyShootingCoroutine);

    }


    private IEnumerator ShootingEnumerator()
    {
           
       
        while (true)
        {

            Vector2 target = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());

            ShootAt(transform.position + new Vector3(target.x-0.5f, upwardsTilt,target.y-0.5f));

           


            yield return delay;
            


        }

    
    
    }
    


}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;


/// <summary>
/// Manages manual shooting through cursor movement and mouse holding.
/// </summary>
public class PlayerShooting : Shooting
{


    private PlayerController controller;
    private Coroutine manuallyShootingCoroutine;

    [SerializeField]
    private LayerMask cameraContactMask;
    [SerializeField]
    private float manualShootingOffsetY;

    [SerializeField]
    private Vector3 cameraColliderSize;


    [SerializeField] private int layerIndex;
    private Renderer playerRenderer;

    protected new void Awake()
    {
        base.Awake();

        Assert.IsTrue(cameraColliderSize.x >= 0 && cameraColliderSize.y >= 0 && cameraColliderSize.z >= 0 && layerIndex >= 0);

    }

    protected new void Start()
    {
        base.Start();
        controller = PlayerController.Instance;

        controller.OnPlayerShootingStart += StartManuallyShooting;
        controller.OnPlayerShootingEnd += StopManuallyShooting;

        CreateCameraCollider();


        
        playerRenderer = GetComponent<Renderer>();


    }

    protected new void OnDestroy()
    {
        base.OnDestroy();

        controller.OnPlayerShootingStart -= StartManuallyShooting;
        controller.OnPlayerShootingEnd -= StopManuallyShooting;
    }



    private void CreateCameraCollider()
    {
        var holder = new GameObject
        {
            layer = layerIndex,
        };




        holder.AddComponent<BoxCollider>();
        var collider = holder.GetComponent<BoxCollider>();
        collider.enabled = true;
        collider.isTrigger = true;
        collider.size = cameraColliderSize;
        collider.transform.position = transform.position + manualShootingOffsetY * Vector3.up;

        holder.transform.parent = transform;




    }





  

    private void StartManuallyShooting()
    {
        
        manuallyShooting = true;
        StopAutoShooting();
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

            var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

      
            Physics.Raycast(ray, out var hit, 100, cameraContactMask);

            Shoot(
                new(transform.position.x, transform.position.y + manualShootingOffsetY, transform.position.z),
                 new(hit.point.x, transform.position.y + manualShootingOffsetY, hit.point.z));


            yield
                
                return delay;



        }



    }



}

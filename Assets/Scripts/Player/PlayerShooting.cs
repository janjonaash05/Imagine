using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

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

    private Renderer projectileRenderer, playerRenderer;

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


        projectileRenderer = projectilePrefab.GetComponent<Renderer>();
        playerRenderer = GetComponent<Renderer>();

        projectileRenderer.material = playerRenderer.material;

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





    protected new void OnDestroy()
    {
        base.OnDestroy();

        controller.OnPlayerShootingStart -= StartManuallyShooting;
        controller.OnPlayerShootingEnd -= StopManuallyShooting;
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

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
            Physics.Raycast(ray, out var hit, 100, cameraContactMask);

            Shoot(
                new(transform.position.x, transform.position.y + manualShootingOffsetY, transform.position.z),
                 new(hit.point.x, transform.position.y + manualShootingOffsetY, hit.point.z));


            yield return delay;



        }



    }



}

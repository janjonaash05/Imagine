using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;



/// <summary>
/// Manages the players's health and overrides certain actions during damage or death. Also checks for health pickups.
/// </summary>
public class PlayerHealth : Health
{

    [SerializeField] private float pickupRadius;

    [SerializeField] private LayerMask pickupMask;
    [SerializeField] private int healthGainedPerPickup;


    public event Action<int,int> OnHealthUpdated;

    public static PlayerHealth Instance { get; private set; }





  


    protected override void MidDeathAction()
    {
        PlayerController.Instance.DisableExternal();
    }


    protected override void AfterDeathAction()
    {
        SceneLoader.LoadMenu();
    }

   
    protected override void MidDamageAction()
    {
        OnHealthUpdated?.Invoke(health,baseHealth);
    }



    protected void FixedUpdate()
    {
        var result = Physics.OverlapSphere(transform.position, pickupRadius, pickupMask, QueryTriggerInteraction.Collide);
        if (result.Length != 0)
        {
            foreach (var collider in result)
            {
                if (collider.gameObject == null) continue;
                health += (health + healthGainedPerPickup <= baseHealth) ? healthGainedPerPickup : baseHealth - health;
                OnHealthUpdated?.Invoke(health, baseHealth);
                Destroy(collider.gameObject);

            }
        }

    }

    private void Start()
    {
        OnHealthUpdated?.Invoke(baseHealth, baseHealth);
    }

    private new void Awake()
    {
        base.Awake();
        Assert.IsTrue(healthGainedPerPickup >= 0, "health gained per pickup must be non-negative");

        if (Instance == null) Instance = this;
    }


    private new void OnDestroy()
    {
        base.OnDestroy();
    }
}

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



    private new void Awake()
    {
        base.Awake();
        Assert.IsTrue(pickupMask > 0 && healthGainedPerPickup >= 0);
    }



    protected override void MidDeathAction()
    {
        PlayerController.Instance.DisableExternal();
    }


    protected override void AfterDeathAction()
    {

        SceneLoader.LoadMenu();


    }

     private void Start()
    {
        PlayerHUD.Instance.SetupHPLabel(baseHealth);
    }

    protected override void MidDamageAction()
    {

        PlayerHUD.Instance.UpdateHPLabel(health);
    }



    protected void FixedUpdate()
    {
        var result = Physics.OverlapSphere(transform.position, pickupRadius, pickupMask, QueryTriggerInteraction.Collide);
        if (result.Length != 0)
        {
            foreach (var collider in result)
            {
                if(collider.gameObject == null) continue;
                health += (health + healthGainedPerPickup <= baseHealth) ? healthGainedPerPickup : baseHealth - health;
                PlayerHUD.Instance.UpdateHPLabel(health);
                Destroy(collider.gameObject);

            }
        }
       


    }
}

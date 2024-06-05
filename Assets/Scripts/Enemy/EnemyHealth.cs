using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


/// <summary>
/// Manages the enemy's health and overrides certain actions during damage or death.
/// </summary>
public class EnemyHealth : Health
{

    [SerializeField] private GameObject healthPickupPrefab;
    [SerializeField] private int healthPickupAmount;






    private Renderer rend;


    public static event Action<string> OnDeath;

    
  

    private new void Awake()
    {
        base.Awake();
        Assert.IsNotNull(healthPickupPrefab);
        Assert.IsTrue(healthPickupAmount >= 0);

        rend = GetComponent<Renderer>();
    }

    protected override void MidDamageAction()
    {
        if (rend != null) 
        {
            var color = rend.material.color;
            var newColor = new Color(color.r,color.g, color.b,  health/ (float) baseHealth);
            rend.material.color = newColor;
        }
    }



    protected override void MidDeathAction()
    {
      //  PlayerHUD.Instance.AddKill(GetComponent<EnemyID>().Type);





        var r = new System.Random();
        for (int i = 0; i < healthPickupAmount; i++)
        {
           var drop = Instantiate(healthPickupPrefab, transform.position,transform.rotation);
           var dropDirection = Vector3.down + Vector3.forward * ( (float) r.NextDouble() - 0.5f) + Vector3.left * ((float)r.NextDouble() - 0.5f);
           drop.GetComponent<Rigidbody>().AddForce(dropDirection, ForceMode.Impulse);
        }

    }

    protected override void AfterDeathAction()
    {

        OnDeath?.Invoke(GetComponent<EnemyID>().Name);
        Destroy(gameObject);
    }


}

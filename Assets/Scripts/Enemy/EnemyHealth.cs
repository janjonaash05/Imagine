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

    [SerializeField] private KillsManagerScriptableObject killsManager;

    private Renderer rend;
    private Material changingMat;
    public static event Action<string> OnDeath;





 

    protected override void MidDamageAction()
    {
        if (rend != null)
        {
            var color = changingMat.color;
            var newColor = new Color(color.r, color.g, color.b, health / (float)baseHealth);
            changingMat.color = newColor;
        }
    }

  
    protected override void MidDeathAction()
    {

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

        killsManager.KillNamedEnemy(GetComponent<EnemyID>().Name);
        Destroy(gameObject);
    }

    private new void Awake()
    {
        base.Awake();

        Assert.IsNotNull(killsManager, "kills manager is not null");
        Assert.IsNotNull(healthPickupPrefab, "health pickup prefab must not be null");
        Assert.IsTrue(healthPickupAmount >= 0, "health pickup amount must be non-negative");

        rend = GetComponent<Renderer>();
        changingMat = rend.material;
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        Destroy(changingMat);
    }


}

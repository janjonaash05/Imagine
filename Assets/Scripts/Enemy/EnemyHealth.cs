using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyHealth : Health
{

    [SerializeField] private GameObject healthPickupPrefab;
    [SerializeField] private int healthPickupAmount;


    protected override void MidDeathAction()
    {
        PlayerHUD.Instance.AddKill(GetComponent<EnemyID>().Type);

        var r = new System.Random();
        for (int i = 0; i < healthPickupAmount; i++)
        {
           var drop = Instantiate(healthPickupPrefab, transform.position,transform.rotation);
           var dropDirection = Vector3.down + Vector3.forward * ( (float) r.NextDouble() - 0.5f) + Vector3.left * ((float)r.NextDouble() - 0.5f);
           drop.GetComponent<Rigidbody>().AddForce(dropDirection, ForceMode.Impulse);
        }

    }


    
}

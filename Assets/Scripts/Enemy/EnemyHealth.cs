using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyHealth : Health
{

   



    

    public override void AfterDeathAction()
    {
        /*
        if (inDeath) return;
        inDeath = true;

        


     

        var rend = deathPs.GetComponent<ParticleSystemRenderer>();
        rend.material = GetComponent<Renderer>().material;

        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Shooting>());
        Destroy(GetComponent<Renderer>());
        StartCoroutine(PlayDeathPS());
        */


        PlayerHUD.Instance.AddKill(GetComponent<EnemyID>().Type);
    }

    /*
    private IEnumerator PlayDeathPS()
    {
        var emission = deathPs.emission;
        emission.enabled = true;

        deathPs.Play();
        yield return new WaitForSeconds(deathPs.main.duration);
        Destroy(gameObject);


    }
    */
}

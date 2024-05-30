using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{

    [SerializeField] private ParticleSystem deathPs;

   private bool inDeath = false;


    public override void Death()
    {
        if (inDeath) return;
        inDeath = true;

        PlayerHUD.Instance.AddKill(GetComponent<EnemyID>().Type);

        var rend = deathPs.GetComponent<ParticleSystemRenderer>();
        rend.material = GetComponent<Renderer>().material;

        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Shooting>());
        Destroy(GetComponent<Renderer>());
        StartCoroutine(PlayDeathPS());
    }

    private IEnumerator PlayDeathPS()
    {
        var emission = deathPs.emission;
        emission.enabled = true;

        deathPs.Play();
        yield return new WaitForSeconds(deathPs.main.duration);
        Destroy(gameObject);


    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;


/// <summary>
/// Manages health, damage and eventually death behaviour with a ParticleSystem.
/// </summary>
public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int baseHealth;
    [SerializeField] private ParticleSystem deathPS;
    protected int health;
    protected void Awake()
    {
        health = baseHealth;

        Assert.IsTrue(baseHealth > 0);
        Assert.IsNotNull(deathPS);

        var rend = deathPS.GetComponent<ParticleSystemRenderer>();
        rend.material = new Material( GetComponent<Renderer>().material);
    }

    private bool inDeath = false;

    public void Damage(int damage)
    {
        if (inDeath) return;

        health -= (health -damage >= 0) ? damage : damage-health;

        MidDamageAction();

        if (health <= 0)
        {
           
            inDeath = true;

            var colliders = GetComponents<Collider>().ToList();
            foreach (var collider in colliders)
            {
                Destroy(collider);
            }
            Destroy(GetComponent<Shooting>());
            Destroy(GetComponent<Renderer>());
            StartCoroutine(PlayDeathPS());
        }

    }





    private IEnumerator PlayDeathPS()
    {
        var emission = deathPS.emission;
        emission.enabled = true;

        deathPS.Play();


        MidDeathAction();
        yield return new WaitForSeconds(deathPS.main.duration);
        AfterDeathAction();

    }



    protected virtual void AfterDeathAction() { }

    protected virtual void MidDamageAction() { }

    protected virtual void MidDeathAction() { }




}

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
    protected int health;
    [SerializeField] private ParticleSystem deathPS;

    private bool inDeath = false;

    private Material projectileMat;







    public void Damage(int damage)
    {
        if (inDeath) return;

        health -= (health - damage >= 0) ? damage : damage - health;

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


    protected void Awake()
    {
        health = baseHealth;

        Assert.IsTrue(baseHealth > 0, "base health must be positive");
        Assert.IsNotNull(deathPS, "death particle system must not be null");


        projectileMat = new Material(GetComponent<Renderer>().sharedMaterial);

        var rend = deathPS.GetComponent<ParticleSystemRenderer>();
        rend.sharedMaterial = projectileMat;
    }



    protected void OnDestroy()
    {
        Destroy(projectileMat);
    }



    protected virtual void AfterDeathAction() { }

    protected virtual void MidDamageAction() { }

    protected virtual void MidDeathAction() { }

    private IEnumerator PlayDeathPS()
    {
        var emission = deathPS.emission;
        emission.enabled = true;

        deathPS.Play();


        MidDeathAction();
        yield return new WaitForSeconds(deathPS.main.duration);
        AfterDeathAction();

    }







}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int baseHealth;
    [SerializeField] private ParticleSystem deathPS;
    protected int health;
    private void Awake()
    {
        health = baseHealth;


        Assert.IsNotNull(deathPS);

        var rend = deathPS.GetComponent<ParticleSystemRenderer>();
        rend.material = GetComponent<Renderer>().material;
    }

    private bool inDeath = false;

    public void Damage()
    {
        health--;
        MidDamageAction();

        if (health <= 0) 
        {
            if (inDeath) return;
            inDeath = true;

            Destroy(GetComponent<Collider>());
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
    yield return new WaitForSeconds(deathPS.main.duration);
    Destroy(gameObject);


        AfterDeathAction();

}

    

    public virtual void AfterDeathAction() { }

    public virtual void MidDamageAction() { }




}

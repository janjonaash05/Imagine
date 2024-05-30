using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int baseHealth;
    protected int health;
    private void Start()
    {
        health = baseHealth;
    }



    public void Damage() 
    {
        health--;

        if (health <= 0) Death();
    }

    public abstract void Death();

    public virtual void MidDamageAction() { }




}

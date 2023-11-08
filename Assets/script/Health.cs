using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float currenHealth;
    
    void Start()
    {
        currenHealth = maxHealth;
    }

    public void GainHealth(float healthPlus)
    {

        currenHealth += healthPlus;

        print(currenHealth);

    }

    public void LoseHealth(float healthMinus)
    {

        currenHealth -= healthMinus;

        print(currenHealth);

        if (currenHealth <= 0)
        {

            OnDeath();

        }

    }

    public float GetInfoCurrentHealth()
    {

        return currenHealth;

    }

    public float GetInfoMaxHealth()
    {

        return maxHealth;

    }

    protected abstract void OnDeath();
}

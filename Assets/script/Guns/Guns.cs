using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Guns : MonoBehaviour
{
    [SerializeField]
    protected int iD;
    public int ID { get => iD;}

    public float maxCooldownTime = 1f; // encapsular
    private float cooldownTime;

    public int bulletsPerShoot = 1;

    //public Transform[] shootPoints;

    public float damage = 3;

    public bool isReady => cooldownTime >= maxCooldownTime;


    protected void Awake()
    {

        cooldownTime = maxCooldownTime;

    }


    protected void Update()
    {
        if (isReady == false)
        {

            cooldownTime += Time.deltaTime;

        }

    }

    public void Activate()
    {

        if (isReady == true)
        {

            OnActivate();
            cooldownTime = 0;

        }

    }

    public virtual void OnHit(EnemyHealth enemy)
    {

        enemy.LoseHealth(damage);

    }

    public void AtoDestroy()
    {

        Destroy(gameObject);

    }

    protected abstract void OnActivate();

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SecundayGun : Guns
{
    
    public int maxAmmo; // encapsular
    
    protected int currentAmmo { get; set; }

    public Transform[] shootDirection; // encapsular

    public GameObject gunPrefab; // encapsular

    protected GunManager manager;

    protected virtual void Start()
    {

        currentAmmo = maxAmmo;

        

    }

    protected override void OnActivate()
    {

        if (currentAmmo > 0)
        {
            currentAmmo--;

            for (int i = 0; i < bulletsPerShoot; i++)
            {

                ShootProjectile(shootDirection[i]);


            }

        }

    }

    protected void CheckAmount()
    {


        if (currentAmmo <= 0)
        {

            manager = transform.parent.parent.GetComponent<GunManager>();

            GameObject newGun = Instantiate(gunPrefab);
            manager.PickUpGuns(newGun.GetComponent<Guns>());

        }

    }

    protected abstract void ShootProjectile(Transform direction);

}

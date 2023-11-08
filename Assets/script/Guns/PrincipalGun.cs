using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalGun : Guns
{
    public Transform[] shootDirection; // encapsular

    public GameObject projectilePrefab; // encapsular

    protected override void OnActivate()
    {
        for (int i = 0; i < bulletsPerShoot; i++)
        {

            ShootProjectile(shootDirection[i]);


        }
    }

    protected void ShootProjectile(Transform _position)
    {

        

        GameObject bullet = Instantiate(projectilePrefab, _position.position, _position.rotation);

        bullet.GetComponent<Harpoon>().gun = this;

    }

}

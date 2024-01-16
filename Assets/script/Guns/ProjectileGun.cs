using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : SecundayGun
{

    public GameObject projectilePrefab; // encapsular

    protected override void ShootProjectile(Transform _position)
    {
        cShake.Shake(shakeDistance, shakeTime);
        GameObject bullet = Instantiate(projectilePrefab, _position.position, _position.rotation);

        bullet.GetComponent<Proyectile>().gun = this;

        CheckAmount();
    }

}

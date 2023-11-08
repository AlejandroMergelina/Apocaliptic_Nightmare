using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : MonoBehaviour
{
    public GameObject gunPrefab;
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        GunManager manager = other.GetComponent<GunManager>();

        if (manager == null)
            return;

        GameObject newGun = Instantiate(gunPrefab);
        manager.PickUpGuns(newGun.GetComponent<Guns>());

        Destroy(gameObject);

    }

}

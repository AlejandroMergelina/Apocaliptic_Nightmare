using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : MonoBehaviour
{
    [SerializeField]
    float healthPlus;

    private PlayerHealth pH;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            pH = collision.GetComponent<PlayerHealth>();
            if(pH.GetInfoCurrentHealth() < pH.GetInfoMaxHealth())
                pH.GainHealth(healthPlus);

            Destroy(gameObject);
        }
       

    }
}

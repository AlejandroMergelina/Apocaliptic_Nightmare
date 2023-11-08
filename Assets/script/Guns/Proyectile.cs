using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{

    [SerializeField]
    float velocity = 10;

    public Guns gun;

    private float time = 3; 

    void Update()
    {

        Move();

        AtoDeath();
        
    }

    void Move()
    {

        transform.position += transform.up * Time.deltaTime * velocity;

    }

    void AtoDeath()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {

            Destroy(gameObject);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.tag == "Enemy")
        {

            EnemyHealth health = collision.GetComponent<EnemyHealth>();
            if (health != null)
            {

                gun.OnHit(health);

            }
            else
            {

                Enemy enemy = collision.GetComponent<Enemy>();

                enemy.OnDeath();

            }
        }
    }

}

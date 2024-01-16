using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{

    [SerializeField]
    protected float velocity;

    public Guns gun;
    [SerializeField]
    protected float time = 3; 

    void Update()
    {

        Move();

        AtoDeath();
        
    }

    void Move()
    {

        transform.position += transform.up * Time.deltaTime * velocity;

    }

    protected virtual void AtoDeath()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {

            Destroy(gameObject);

        }

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

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
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : RaycastController
{

    Vector2 grewY = new Vector2 (0f,5f);
    float offsetY;

    collisionInfo collisions;
 

    SpriteRenderer sprite;
    BoxCollider2D boxCollider;

    public Guns gun;

    private float contador;

    public override void Start()
    {
        base.Start();

        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    
    void Update()
    {
        CalculateRaySpacing();
        UpdateRaycastOrigins();
        if (collisions.above)
        {
            
            grewY = new Vector2(0f, 0f);
                        
        }
        tiempoVida();
        Move(grewY * Time.deltaTime);
    }

    void tiempoVida()
    {

        contador += Time.deltaTime;
        
        if (contador >= 3)
        {

            Destroy(gameObject);

        }

    }

    void Move(Vector2 grew)
    {
        VerticalCollisions(ref grew);

        

        offsetY = grew.y / 2;

        boxCollider.size += grew;
        boxCollider.offset += new Vector2(0f, offsetY);
        sprite.size += grew;

        
        
    }

    void VerticalCollisions(ref Vector2 grewY)
    {
        collisions.Reset();
        float directionY = Mathf.Sign(grewY.y);
        float rayLengh = Mathf.Abs(grewY.y) + skinWidh;

        for (int i = 0; i < verticalRayCount; i++)
        {

            Vector2 rayOrigin = raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLengh, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLengh, Color.red);

            if (hit)
            {

                collisions.above = true;
                grewY.y = (hit.distance - skinWidh) * directionY;
                rayLengh = hit.distance;



            }

        }

    }

    

    struct collisionInfo
    {
        public bool above;


        public void Reset()
        {

            above = false;
            
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

                //gun.OnHit(health);

            }
            else
            {

                Enemy enemy = collision.GetComponent<Enemy>();

                enemy.OnDeath();

            }
        }

        

    }

}

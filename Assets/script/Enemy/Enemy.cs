using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float jumpHeight = 4;
    [SerializeField]
    private float timeToJumpApex = 0.4f;
    [SerializeField]
    private float moveSpeed = 6f;

    [SerializeField]
    private int damage;
    [SerializeField]
    private int nValue;

    private float gravity;
    private float jumpVelocity;
    private Vector3 velocity;

    EnemyController controller;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject[] itemsPrefabs;

    [SerializeField]
    private GameControllerLevel gC;

    void Start()
    {
        

        controller = GetComponent<EnemyController>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + " Jump Velocity: " + jumpVelocity); gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        
    }

    
    void Update()
    {
        CalculateVelocity();

        controller.Move(velocity * Time.deltaTime);

        if (controller.collisions.above || controller.collisions.below)
        {

            velocity.y = 0;
            if (controller.collisions.below)
            {

                OnJump();

            }
            

        }

        if (controller.collisions.left || controller.collisions.right)
        {

            velocity.x = 0;
            moveSpeed = -moveSpeed;

        }
    }

    public void OnJump()
    {

        if (controller.collisions.below)
        {
            

            velocity.y = jumpVelocity;

            
        }

    }

    void CalculateVelocity()
    {

        velocity.x = moveSpeed;
        velocity.y += gravity * Time.deltaTime;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            PlayerHealth health = collision.GetComponent<PlayerHealth>();
            health.LoseHealth(damage);
        }        
    }
    
    public void OnDeath()
    {

        Destroy(gameObject);
        if (enemyPrefab != null)
        {
            GameObject clon = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            GameObject clon2 = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Enemy enemy = clon.GetComponent<Enemy>();
            Enemy enemy2 = clon2.GetComponent<Enemy>();
            enemy2.SetDirection(-1);

            enemy.SetControllerInfo(gC);
            enemy2.SetControllerInfo(gC);
        }

        int decision = Random.Range(0, 4);

        if(decision == 0)
        {

            Instantiate(itemsPrefabs[0], gameObject.transform.position, gameObject.transform.rotation);

        }
        else if (decision == 1)
        {

            Instantiate(itemsPrefabs[1], gameObject.transform.position, gameObject.transform.rotation);

        }
        else if (decision == 2)
        {

            Instantiate(itemsPrefabs[2], gameObject.transform.position, gameObject.transform.rotation);

        }
        

        gC.NEnemy(transform, nValue);
    }

    public void SetDirection(int direct)
    {
        

        moveSpeed *= direct;
        
    }

    public float GetInfoDirection()
    {
        
        return moveSpeed;
        
    }

    public void SetControllerInfo(GameControllerLevel gameController)
    {
        
        this.gC = gameController;
        
    }

}

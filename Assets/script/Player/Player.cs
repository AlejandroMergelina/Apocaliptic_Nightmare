using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerController))]

public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpHeight = 4;
    [SerializeField]
    private float timeToJumpApex = 0.4f;
    [SerializeField]
    private float moveSpeed = 6f;

    private float gravity;
    private float jumpVelocity;
    [SerializeField]
    private Vector3 velocity;

    private PlayerController controller;
    public PlayerController Controller { get => controller;}

    private Vector2 directionalInput;

    [SerializeField]
    private float coyoteTime;
    [SerializeField]
    private float coyoteTimer;

    //[SerializeField]
    //private Collider2D collisionTopLadert;

    void Start()
    {
        controller = GetComponent<PlayerController>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + " Jump Velocity: " + jumpVelocity);
    
    }

    void Update()
    {

        CalculateVelocity();


        if (Controller.getInfoCollision().below == false)
        {
            
            coyoteTimer -= Time.deltaTime;

        }
        else
        {

            coyoteTimer = coyoteTime;

        }

        if (controller.getInfoCollision().canClimbingLader && directionalInput.y != 0)
        {

            Controller.SetInfoCollisionClimbingLader(true);
            
        }

        

        if (Controller.getInfoCollision().below && directionalInput.y == -1)
        {

            Controller.SetInfoCollisionClimbingLader(false);

        }

        if (Controller.getInfoCollision().canDownLader && directionalInput.y < 0)
        {

            Controller.SetInfoCollisioIsTryingDonwLader(true);

        }
        else
        {

            Controller.SetInfoCollisioIsTryingDonwLader(false);

        }

        if (Controller.getInfoCollision().isClimbingLader)
        {

            velocity.y = directionalInput.y * moveSpeed;
            velocity.x = 0;


            Controller.Move(velocity * Time.deltaTime);
            velocity.y = 0;
        }
        else
        {
            Controller.Move(velocity * Time.deltaTime);
            if (Controller.getInfoCollision().above || Controller.getInfoCollision().below)
            {
                if (Controller.getInfoCollision().slidingDownMaxSlope)
                {
                    velocity.y += Controller.getInfoCollision().slopeNormal.y * -gravity * Time.deltaTime;
                }
                else
                {
                    velocity.y = 0;
                }

            }
        }
        
    }

    public void SetDirectionalInput(Vector2 input)
    {

        directionalInput = input;

    }

    public void OnJumpInput()
    {

        if (coyoteTimer > 0)
        {
            if (Controller.getInfoCollision().slidingDownMaxSlope)
            {
                
                if (directionalInput.x != -Mathf.Sign(Controller.getInfoCollision().slopeNormal.x))
                {

                    velocity.y = jumpVelocity * Controller.getInfoCollision().slopeNormal.y;
                    velocity.x = jumpVelocity * Controller.getInfoCollision().slopeNormal.x;

                }

            }
            else
            {

                velocity.y = jumpVelocity;

            }
        }

    }

    

    void CalculateVelocity()
    {

        velocity.x = directionalInput.x * moveSpeed;
        velocity.y += gravity * Time.deltaTime;

    }

}

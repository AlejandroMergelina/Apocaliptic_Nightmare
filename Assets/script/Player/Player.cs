using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerControler))]

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

    private PlayerControler controller;

    private Vector2 directionalInput;

    [SerializeField]
    private Collider2D collisionTopLadert;

    void Start()
    {
        controller = GetComponent<PlayerControler>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + " Jump Velocity: " + jumpVelocity);
    
    }

    void Update()
    {

        CalculateVelocity();

        if (controller.getInfoCollision().canClimbingLader && directionalInput.y != 0)
        {

            controller.SetInfoCollisionClimbingLader(true);
            
        }

        

        if (controller.getInfoCollision().below && directionalInput.y == -1)
        {

            controller.SetInfoCollisionClimbingLader(false);

        }

        if (controller.getInfoCollision().canDownLader && directionalInput.y < 0)
        {

            controller.SetInfoCollisioIsTryingDonwLader(true);

        }
        else
        {

            controller.SetInfoCollisioIsTryingDonwLader(false);

        }

        if (controller.getInfoCollision().isClimbingLader)
        {

            velocity.y = directionalInput.y * moveSpeed;
            velocity.x = 0;


            controller.Move(velocity * Time.deltaTime);
            velocity.y = 0;
        }
        else
        {
            controller.Move(velocity * Time.deltaTime);
            if (controller.getInfoCollision().above || controller.getInfoCollision().below)
            {
                if (controller.getInfoCollision().slidingDownMaxSlope)
                {
                    velocity.y += controller.getInfoCollision().slopeNormal.y * -gravity * Time.deltaTime;
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

        if (controller.getInfoCollision().below)
        {
            if (controller.getInfoCollision().slidingDownMaxSlope)
            {
                
                if (directionalInput.x != -Mathf.Sign(controller.getInfoCollision().slopeNormal.x))
                {

                    velocity.y = jumpVelocity * controller.getInfoCollision().slopeNormal.y;
                    velocity.x = jumpVelocity * controller.getInfoCollision().slopeNormal.x;

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

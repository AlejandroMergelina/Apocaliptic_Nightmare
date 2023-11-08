using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControler : RaycastController
{

    [SerializeField]
    private float maxSlopeAngle = 55f;

    private CollisionInfo collisions;

    [SerializeField]
    private Vector3 velocity;

    [SerializeField]
    private float gravity = -30;

    public override void Start()
    {

        base.Start();


    }

    void Update()
    {

        CalculateVelocity();

        Move(velocity * Time.deltaTime);
        if (collisions.above || collisions.below)
        {
            if (collisions.slidingDownMaxSlope)
            {
                velocity.y += collisions.slopeNormal.y * -gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
            }

        }
       
    }

    void CalculateVelocity()
    {

        velocity.x = 0;
        velocity.y += gravity * Time.deltaTime;

    }

    public void Move(Vector2 moveAmount)
    {

        UpdateRaycastOrigins();
        collisions.Reset();
        collisions.moveAmountOld = moveAmount;

        if (moveAmount.y < 0)
        {

            RaycastHit2D maxSlopeHitLeft = Physics2D.Raycast(raycastOrigins.bottomLeft, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidh, collisionMask);
            RaycastHit2D maxSlopeHitRight = Physics2D.Raycast(raycastOrigins.bottomRight, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidh, collisionMask);
            if (maxSlopeHitLeft ^ maxSlopeHitRight)
            {
                SlideDownMaxSlope(maxSlopeHitLeft, ref moveAmount);
                SlideDownMaxSlope(maxSlopeHitRight, ref moveAmount);
            }

        }



        if (moveAmount.x != 0)
        {

            HorizontalColision(ref moveAmount);

        }

        if (moveAmount.y != 0)
        {

            VerticalCollision(ref moveAmount);

        }





        transform.Translate(moveAmount);

    }

    void HorizontalColision(ref Vector2 moveAmount)
    {

        float directionX = Mathf.Sign(moveAmount.x);
        float rayLengh = Mathf.Abs(moveAmount.x) + skinWidh;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLengh, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLengh, Color.red);

            if (hit)
            {

                float slopeAngel = Vector2.Angle(hit.normal, Vector2.up);

                if (i == 0 && slopeAngel <= maxSlopeAngle)
                {
                   
                    float distanceToSlopeStart = 0;
                    if (slopeAngel != collisions.slopeAngleOld)
                    {

                        distanceToSlopeStart = hit.distance - skinWidh;
                        moveAmount.x -= distanceToSlopeStart * directionX;

                    }
                    
                    moveAmount.x += distanceToSlopeStart * directionX;
                }

                if ( slopeAngel > maxSlopeAngle)
                {
                    moveAmount.x = (hit.distance - skinWidh) * directionX;
                    rayLengh = hit.distance;

                   

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
            }

        }

    }

    void VerticalCollision(ref Vector2 moveAmount)
    {

        float directionY = Mathf.Sign(moveAmount.y);
        float rayLengh = Mathf.Abs(moveAmount.y) + skinWidh;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLengh, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLengh, Color.red);

            if (hit)
            {

                moveAmount.y = (hit.distance - skinWidh) * directionY;
                rayLengh = hit.distance;

               

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;

            }

        }

        

    }


    

    void SlideDownMaxSlope(RaycastHit2D hit, ref Vector2 moveAmount)
    {

        if (hit)
        {

            float slopeAngel = Vector2.Angle(hit.normal, Vector2.up);
            if (slopeAngel > maxSlopeAngle)
            {

                moveAmount.x = hit.normal.x * (Mathf.Abs(moveAmount.y) - hit.distance) / Mathf.Tan(slopeAngel * Mathf.Deg2Rad);

                collisions.slopeAngle = slopeAngel;

                collisions.slidingDownMaxSlope = true;

                collisions.slopeNormal = hit.normal;

            }
        }

    }


    public CollisionInfo getInfoCollision()
    {

        return collisions;

    }

    public struct CollisionInfo
    {

        public bool above, below; // encapsular
        public bool left, right; // encapsular

        public bool slidingDownMaxSlope; // encapsular

        public float slopeAngle, slopeAngleOld; // encapsular
        public Vector2 slopeNormal; // encapsular
        public Vector3 moveAmountOld; // encapsular

        public void Reset()
        {

            above = below = false;
            left = right = false;
            
            slidingDownMaxSlope = false;
            slopeNormal = Vector2.zero;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;

        }

    }
}

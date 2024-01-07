using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : RaycastController
{
    [SerializeField]
    private float maxSlopeAngle = 55f;

    private CollisionInfo collisions;

    public override void Start()
    {

        base.Start();

    }

    public void Move(Vector2 moveAmount)
    {

        UpdateRaycastOrigins();
        collisions.Reset();
        collisions.moveAmountOld = moveAmount;

        if (moveAmount.y < 0)
        {

            DescendSlope(ref moveAmount);

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

                float slopeAngel =Vector2.Angle(hit.normal, Vector2.up);

                if (i == 0 && slopeAngel <= maxSlopeAngle)
                {
                    if(collisions.descendingSlope)
                    {

                        collisions.descendingSlope = false;
                        moveAmount = collisions.moveAmountOld;

                    }
                    float distanceToSlopeStart = 0;
                    if (slopeAngel != collisions.slopeAngleOld)
                    {

                        distanceToSlopeStart = hit.distance - skinWidh;
                        moveAmount.x -= distanceToSlopeStart * directionX;

                    }
                    ClimbSlope(ref moveAmount, slopeAngel, hit.normal);
                    moveAmount.x += distanceToSlopeStart * directionX;
                }

                if (!collisions.climbingSlope || slopeAngel > maxSlopeAngle)
                {
                    moveAmount.x = (hit.distance - skinWidh) * directionX;
                    rayLengh = hit.distance;

                    if (collisions.climbingSlope)
                    {

                        moveAmount.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x);

                    }

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

                if (collisions.climbingSlope)
                {

                    moveAmount.x = moveAmount.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(moveAmount.x);

                }

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;

            }

        }

        if (collisions.climbingSlope)
        {

            float directionX = Mathf.Sign(moveAmount.x);
            rayLengh = Mathf.Abs(moveAmount.x) + skinWidh;
            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * moveAmount.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLengh, collisionMask);

            if (hit)
            {

                float slopAngel = Vector2.Angle(hit.normal, Vector2.up);
                if (slopAngel != collisions.slopeAngle)
                {

                    moveAmount.x = (hit.distance - skinWidh) * directionX;
                    collisions.slopeAngle = slopAngel;
                    collisions.slopeNormal = hit.normal;

                }

            }

        }

    }

    void ClimbSlope(ref Vector2 moveAmount, float slopeAngel,Vector2 slopeNormal)
    {

        float moveDistance = Mathf.Abs(moveAmount.x);
        float climbVelocityY = Mathf.Sin(slopeAngel * Mathf.Deg2Rad) * moveDistance;

        if (moveAmount.y <= climbVelocityY)
        {

            moveAmount.y = climbVelocityY;
            moveAmount.x = Mathf.Cos(slopeAngel * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngel;
            collisions.slopeNormal = slopeNormal;

        }
        


    }

    void DescendSlope(ref Vector2 moveAmount)
    {

        RaycastHit2D maxSlopeHitLeft = Physics2D.Raycast(raycastOrigins.bottomLeft, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidh, collisionMask);
        RaycastHit2D maxSlopeHitRight = Physics2D.Raycast(raycastOrigins.bottomRight, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidh, collisionMask);
        if (maxSlopeHitLeft)
        {
            SlideDownMaxSlope(maxSlopeHitLeft, ref moveAmount);
            
        }
        else if (maxSlopeHitRight)
        {

            SlideDownMaxSlope(maxSlopeHitRight, ref moveAmount);
        }

        if (!collisions.slidingDownMaxSlope)
        {

            float directionX = Mathf.Sign(moveAmount.x);
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

            if (hit)
            {

                float slopeAngel = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngel != 0 && slopeAngel <= maxSlopeAngle)
                {

                    if (Mathf.Sign(hit.normal.x) == directionX)
                    {

                        if (hit.distance - skinWidh <= Mathf.Tan(slopeAngel * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x))
                        {

                            float moveDistance = Mathf.Abs(moveAmount.x);
                            float descendVelocityY = Mathf.Sin(slopeAngel * Mathf.Deg2Rad) * moveDistance;
                            moveAmount.x = Mathf.Cos(slopeAngel * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
                            moveAmount.y -= descendVelocityY;

                            collisions.slopeAngle = slopeAngel;
                            collisions.descendingSlope = true;
                            collisions.below = true;
                            collisions.slopeNormal = hit.normal;

                        }

                    }

                }

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
                //if (slopeAngel < 45)
                    //moveAmount.x = -moveAmount.x;
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

    public void SetInfoCollisionCanClimbingLader(bool climbingLader)
    {

        collisions.canClimbingLader = climbingLader;

    }

    public void SetInfoCollisionClimbingLader(bool climbingLader)
    {

        collisions.isClimbingLader = climbingLader;

    }

    public void SetInfoCollisionDownLader(bool canDownLader)
    {

        collisions.canDownLader = canDownLader;

    }

    public void SetInfoCollisioIsTryingDonwLader(bool isTryingDonwLader)
    {

        collisions.isTryingDonwLader = isTryingDonwLader;

    }

    public struct CollisionInfo
    {

        public bool above, below; // encapsular
        public bool left, right; // encapsular

        public bool climbingSlope; // encapsular
        public bool descendingSlope;
        public bool slidingDownMaxSlope; // encapsular

        public float slopeAngle, slopeAngleOld; // encapsular
        public Vector2 slopeNormal; // encapsular
        public Vector3 moveAmountOld; // encapsular

        public bool canClimbingLader;
        public bool canDownLader;//<----
        public bool isTryingDonwLader;
        public bool isClimbingLader;
        public void Reset()
        {

            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;
            slidingDownMaxSlope = false;
            slopeNormal = Vector2.zero;



        slopeAngleOld = slopeAngle;
            slopeAngle = 0;

        }

    }

}

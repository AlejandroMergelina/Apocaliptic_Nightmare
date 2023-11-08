using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : RaycastController
{

    public CollisionInfo collisions;

    public void Move(Vector2 moveAmount)
    {

        UpdateRaycastOrigins();
        collisions.Reset();
        collisions.moveAmountOld = moveAmount;





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

                    moveAmount.x = (hit.distance - skinWidh) * directionX;
                    rayLengh = hit.distance;

                   

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
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

    public struct CollisionInfo
    {

        public bool above, below;
        public bool left, right;

        public Vector3 moveAmountOld;

        public void Reset()
        {

            above = below = false;
            left = right = false;

        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class RaycastController : MonoBehaviour
{
    public LayerMask collisionMask;

    public const float skinWidh = 0.015f;
    const float dstBetweenRaySpacing = 0.05f;
    
    protected int horizontalRayCount;//toketeo

    protected int verticalRayCount;


    protected float horizontalRaySpacing;
    
    protected float verticalRaySpacing;

    [HideInInspector]
    public BoxCollider2D collidera;
    public RaycastOrigins raycastOrigins;

    public virtual void Start()
    {
        collidera = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
        
    }

    public void UpdateRaycastOrigins()
    {

        Bounds bounds = collidera.bounds;
        bounds.Expand(skinWidh * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpacing()
    {

        Bounds bounds = collidera.bounds;
        bounds.Expand(skinWidh * -2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;


        horizontalRayCount = Mathf.RoundToInt(boundsHeight / dstBetweenRaySpacing);
        verticalRayCount = Mathf.RoundToInt(boundsWidth / dstBetweenRaySpacing);

        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);

    }



    public struct RaycastOrigins
    {

        public Vector2 topLeft, topRight;

        public Vector2 bottomLeft, bottomRight;

    }

    public int getInfo()
    {

        return horizontalRayCount;

    }
}

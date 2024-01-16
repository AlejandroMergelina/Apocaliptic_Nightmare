using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private PlayerController target;
    [SerializeField]
    private float verticalOffset;
    [SerializeField]
    private float lookAheadDstX;
    [SerializeField]
    private float lookSmoothTimeX; 
    [SerializeField]
    private float verticalSmoothTime;
    [SerializeField]
    private Vector2 focusAreaSize;
    [SerializeField]
    private PlayerInput input;

    private FocusArea focusArea;

    private float currentLookAheadX;
    private float targetLookAheadX;
    private float lookAheadDirX;
    private float smoothLookVelocityX;
    private float smoothVelocityY;

    private bool lookAheadStopped;

    private void Start()
    {

        focusArea = new FocusArea(target.Collider.bounds, focusAreaSize);


    }

    private void LateUpdate()
    {

        focusArea.Update(target.Collider.bounds);
        Vector2 focusPosition = focusArea.Center + Vector2.up * verticalOffset;

        if(focusArea.Velocity.x != 0)
        {

            lookAheadDirX = Mathf.Sign(focusArea.Velocity.x);
            if(Mathf.Sign(input.DirectionalInput.x) == Mathf.Sign(focusArea.Velocity.x) && input.DirectionalInput.x != 0)
            {

                lookAheadStopped = false;
                targetLookAheadX = lookAheadDirX * lookAheadDstX;

            }
            else
            {
                if (!lookAheadStopped)
                {
                    lookAheadStopped = true;
                    targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;

                }

            }
        }

        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

        focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        focusPosition += Vector2.right * currentLookAheadX;
        transform.position = new Vector3(focusPosition.x, focusPosition.y, transform.position.z);
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.Center, focusAreaSize); 
        

    }

    struct FocusArea
    {
        [SerializeField]
        private Vector2 center;
        public Vector2 Center { get => center; set => center = value; }
        [SerializeField]
        private Vector2 velocity;
        public Vector2 Velocity { get => velocity; set => velocity = value; }

        private float left, right;
        private float top, bottom;

        public FocusArea(Bounds targetBounds, Vector2 size)
        {

            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector3.zero;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);

        }

        public void Update(Bounds targetBounds)
        {

            float shiftX = 0;
            if (targetBounds.min.x < left)
            {

                shiftX = targetBounds.min.x - left;

            }
            else if (targetBounds.max.x > right)
            {

                shiftX = targetBounds.max.x - right;

            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {

                shiftY = targetBounds.min.y - bottom;

            }
            else if (targetBounds.max.y > top)
            {

                shiftY = targetBounds.max.y - top;

            }
            top += shiftY;
            bottom += shiftY;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);

        }

    }
}

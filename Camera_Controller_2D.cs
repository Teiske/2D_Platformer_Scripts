using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller_2D : MonoBehaviour {

    public Physics_Controller_2D controller_2D;
    public float verticalOffset;
    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;
    public Vector2 focusAreaSize;

    FocusArea focusArea;

    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    bool lookAheadStopped;

    void Start() {
        focusArea = new FocusArea(controller_2D.boxCollider_2D.bounds, focusAreaSize);
    }

    void LateUpdate() {
        focusArea.Update(controller_2D.boxCollider_2D.bounds);

        Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;

        if (focusArea.velocity.x != 0) {
            lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
            if (Mathf.Sign(controller_2D.playerInput.x) == Mathf.Sign(focusArea.velocity.x) && controller_2D.playerInput.x != 0) {
                lookAheadStopped = false;
                targetLookAheadX = lookAheadDirX * lookAheadDstX;
            }
            else {
                if (!lookAheadStopped) {
                    lookAheadStopped = true;
                    targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;
                }
            }
        }

        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

        focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        focusPosition += Vector2.right * currentLookAheadX;
        transform.position = (Vector3)focusPosition + Vector3.forward * -10;
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.center, focusAreaSize);
    }

    struct FocusArea {
        public Vector2 center;
        public Vector2 velocity;
        float left, right;
        float top,bottom;

        public FocusArea(Bounds targertBounds, Vector2 size) {
            left = targertBounds.center.x - size.x / 2;
            right = targertBounds.center.x + size.x / 2;
            bottom = targertBounds.min.y;
            top = targertBounds.min.y + size.y;

            velocity = Vector2.zero;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
        }
        public void Update(Bounds targetBounds) {
            float shiftX = 0;
            if (targetBounds.min.x < left) {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right) {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom) {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top) {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;

            center = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
}

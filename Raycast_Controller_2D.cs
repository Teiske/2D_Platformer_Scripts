using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_Controller_2D : MonoBehaviour {

    private Enemy_Controller_2D enemy_Controller_2D;

    public LayerMask collisionMask;

    public const float skinWidth = .015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    [HideInInspector]
    public BoxCollider2D boxCollider_2D;
    [HideInInspector]
    public RaycastOrigins raycastOrigins;

    public bool enemy_Hit;

    public void Awake() {
        boxCollider_2D = GetComponent<BoxCollider2D>();
    }

    //public virtual void Start() {
    //    CalculateRaySpacing();
    //}

    public void UpdateRaycastOrigins() {
        Bounds bounds = boxCollider_2D.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft  = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft     = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight    = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpacing() {
        Bounds bounds = boxCollider_2D.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount   = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing   = bounds.size.x / (verticalRayCount   - 1);
    }

    public void PlayerRayCast() {
        float rayLength = 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength);
        Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.cyan);
        if (hit != null && hit.collider != null && hit.distance < 0.9f && hit.collider.tag == "Enemy") {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500f);
            Debug.Log("Squiched Enemy");
            enemy_Controller_2D.move = false;
            Debug.Log(enemy_Controller_2D.move);
        }
        
    }

    public void EnemyRayCast() {
        
    }

    public struct RaycastOrigins {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}

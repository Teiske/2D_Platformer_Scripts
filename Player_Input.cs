using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller_2D))]
public class Player_Input : MonoBehaviour {
   
    [SerializeField]
    float moveSpeed = 6f;
    [SerializeField]
    float accelerationTimeGrounded = .1f;
    [SerializeField]
    float jumpHeight = 4f;
    [SerializeField]
    float accelerationTimeAirbourne = .2f;
    [SerializeField]
    float timeToJumpApex = .4f;
    float gravity;
    float jumpVelocity;
    float velocityXSmoothing;

    Vector3 velocity;

    Controller_2D controller_2D;

    void Start() {
        controller_2D = GetComponent<Controller_2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    void Update() {

        if (controller_2D.collisions.above || controller_2D.collisions.below) {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller_2D.collisions.below) {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller_2D.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne);
        velocity.y += gravity * Time.deltaTime;
        controller_2D.Move(velocity * Time.deltaTime);
    }
}

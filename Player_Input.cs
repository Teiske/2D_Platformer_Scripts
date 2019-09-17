using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller_2D))]
public class Player_Input : MonoBehaviour {
   
    float moveSpeed = 6f;
    float jumpHeight = 4f;
    float timeToJumpApex = .4f;
    float gravity;
    float jumpVelocity;
    Vector3 velocity;

    Controller_2D controller_2D;

    void Start() {
        controller_2D = GetComponent<Controller_2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = gravity * timeToJumpApex;
    }

    void Update() {

        if (controller_2D.collisions.above || controller_2D.collisions.below) {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller_2D.collisions.below) {
            velocity.y = jumpVelocity;
        }

        velocity.x = input.x * moveSpeed;
        velocity.y += gravity * Time.deltaTime;
        controller_2D.Move(velocity * Time.deltaTime);
    }
}

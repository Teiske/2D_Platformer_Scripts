using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Physics_Controller_2D))]
[RequireComponent(typeof(BoxCollider2D))]
//[RequireComponent(typeof(Rigidbody2D))]
public class Player_Input_Controller_2D : Raycast_Controller_2D {

    private Rigidbody2D rigibody_2D;
    private Player_Life_Controller_2D player_Life_Controller_2D;
    private Raycast_Controller_2D raycast_Controller_2D;

    [SerializeField]
    private float movement_Speed;
    [SerializeField]
    private float jump_Force;
    private float move_X;

    private bool isGrounded = false;
    private bool Level_End_Reached = false;

    void Start() {
        rigibody_2D = GetComponent<Rigidbody2D>();
        player_Life_Controller_2D = GetComponent<Player_Life_Controller_2D>();
    }

    void Update() {
        if (Level_End_Reached) {
            StartCoroutine(player_Life_Controller_2D.Die());
        }
    }

    void FixedUpdate() {
        PlayerMovement();
        PlayerRayCast();
    }

    void OnCollisionEnter2D(Collision2D collision_2D) {
        //Debug.Log("Player has collided with " + collision_2D.collider.name);
        if (collision_2D.gameObject.tag == "Ground") {
            isGrounded = true;
        }

        if (collision_2D.gameObject.tag == "Level_End") {
            Level_End_Reached = true;
            Debug.Log(Level_End_Reached);
        }
    }

    private void PlayerMovement() {
        move_X = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded) {
            PlayerJump();
        }

        rigibody_2D.velocity = new Vector2(move_X * movement_Speed, rigibody_2D.velocity.y);
    }

    private void PlayerJump() {
        rigibody_2D.AddForce(Vector2.up * jump_Force);
        isGrounded = false;
    }

    //private void LevelEnd() { 
    //    SceneManager.LoadScene("Test_Scene");
    //}

    //[SerializeField]
    //float moveSpeed = 6f;
    //[SerializeField]
    //float accelerationTimeGrounded = .1f;
    //[SerializeField]
    //float maxJumpHeight = 4f;
    //[SerializeField]
    //float minJumpHeight = 1f;
    //[SerializeField]
    //float accelerationTimeAirbourne = .2f;
    //[SerializeField]
    //float timeToJumpApex = .4f;
    //[SerializeField]
    //float wallSlideSpeedMax = 3;
    //[SerializeField]
    //float wallStickTime = .25f;
    //float timeToWallUnstick;
    //float gravity;
    //float maxJumpVelocity;
    //float minJumpVelocity;
    //float velocityXSmoothing;

    //public Vector2 wallJumpClimb;
    //public Vector2 wallJumpOff;
    //public Vector2 wallLeap;

    //Vector3 velocity;

    //Physics_Controller_2D controller_2D;

    //void Start() {
    //    controller_2D = GetComponent<Physics_Controller_2D>();

    //    gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
    //    maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    //    minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    //}

    //void Update() {
    //    Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    //    int wallDirX = (controller_2D.collisions.left) ? -1 : 1;

    //    float targetVelocityX = input.x * moveSpeed;
    //    velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller_2D.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne);

    //    bool wallSliding = false;
    //    if ((controller_2D.collisions.left || controller_2D.collisions.right) && !controller_2D.collisions.below && velocity.y < 0) {
    //        wallSliding = true;

    //        if (velocity.y < -wallSlideSpeedMax) {
    //            velocity.y = -wallSlideSpeedMax;
    //        }

    //        if (timeToWallUnstick > 0) {
    //            velocityXSmoothing = 0;
    //            velocity.x = 0;

    //            if (input.x != wallDirX && input.x != 0) {
    //                timeToWallUnstick -= Time.deltaTime;
    //            }
    //            else {
    //                timeToWallUnstick = wallStickTime;
    //            }
    //        }
    //        else {
    //            timeToWallUnstick = wallStickTime;
    //        }
    //    }

    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //        if (wallSliding) {
    //            if (wallDirX == input.x) {
    //                velocity.x = -wallDirX * wallJumpClimb.x;
    //                velocity.y = wallJumpClimb.y;
    //            }
    //            else if (input.x == 0) {
    //                velocity.x = -wallDirX * wallJumpOff.x;
    //                velocity.y = wallJumpOff.y;
    //            }
    //            else {
    //                velocity.x = wallDirX * wallLeap.x;
    //                velocity.y = wallLeap.y;
    //            }
    //        }
    //        if (controller_2D.collisions.below) {
    //            velocity.y = maxJumpVelocity;
    //        }
    //    }
    //    if (Input.GetKeyUp(KeyCode.Space)) {
    //        if (velocity.y > minJumpVelocity) {
    //            velocity.y = minJumpVelocity;
    //        }
    //    }

    //    velocity.y += gravity * Time.deltaTime;
    //    controller_2D.Move(velocity * Time.deltaTime, input);

    //    if (controller_2D.collisions.above || controller_2D.collisions.below) {
    //        velocity.y = 0;
    //    }
    //}
}

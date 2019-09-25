using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Physics_Controller_2D))]
public class Player_Input_Controller_2D : MonoBehaviour {
   
    [SerializeField]
    float moveSpeed = 6f;
    [SerializeField]
    float accelerationTimeGrounded = .1f;
    [SerializeField]
    float maxJumpHeight = 4f;
    [SerializeField]
    float minJumpHeight = 1f;
    [SerializeField]
    float accelerationTimeAirbourne = .2f;
    [SerializeField]
    float timeToJumpApex = .4f;
    [SerializeField]
    float wallSlideSpeedMax = 3;
    [SerializeField]
    float wallStickTime = .25f;
    float timeToWallUnstick;
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    float velocityXSmoothing;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    Vector3 velocity;

    Physics_Controller_2D controller_2D;

    void Start() {
        controller_2D = GetComponent<Physics_Controller_2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void Update() { 
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        int wallDirX = (controller_2D.collisions.left) ? -1 : 1;

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller_2D.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne);

        bool wallSliding = false;
        if ((controller_2D.collisions.left || controller_2D.collisions.right) && !controller_2D.collisions.below && velocity.y < 0) {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax) {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0) {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (input.x != wallDirX && input.x != 0) {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else {
                timeToWallUnstick = wallStickTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (wallSliding) {
                if (wallDirX == input.x) {
                    velocity.x = -wallDirX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
                }
                else if (input.x == 0) {
                    velocity.x = -wallDirX * wallJumpOff.x;
                    velocity.y = wallJumpOff.y;
                }
                else {
                    velocity.x = wallDirX * wallLeap.x;
                    velocity.y = wallLeap.y;
                }
            }
            if (controller_2D.collisions.below) {
                velocity.y = maxJumpVelocity;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            if (velocity.y > minJumpVelocity) {
                velocity.y = minJumpVelocity;
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller_2D.Move(velocity * Time.deltaTime, input);

        if (controller_2D.collisions.above || controller_2D.collisions.below) {
            velocity.y = 0;
        }
    }
}

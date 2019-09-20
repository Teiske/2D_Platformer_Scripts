using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller_2D))]
public class Player_Input_2D : MonoBehaviour {
   
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
    [SerializeField]
    float wallSlideSpeedMax = 3;
    [SerializeField]
    float wallStickTime = .25f;
    float timeToWallUnstick;
    float gravity;
    float jumpVelocity;
    float velocityXSmoothing;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    Vector3 velocity;

    Controller_2D controller_2D;

    void Start() {
        controller_2D = GetComponent<Controller_2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
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
        if (controller_2D.collisions.above || controller_2D.collisions.below) {
            velocity.y = 0;
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
                velocity.y = jumpVelocity;
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller_2D.Move(velocity * Time.deltaTime);
    }
}

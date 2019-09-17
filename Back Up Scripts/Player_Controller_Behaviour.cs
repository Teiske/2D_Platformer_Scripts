using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_Behaviour : Physics_Object_Behaviour {

    public float max_Speed  = 7f;
    public float jump_Force = 7f;

    void Start() {
        
    }

    protected override void ComputeVelocity() {

        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded) {
            velocity.y = jump_Force;
        }
        else if (Input.GetButtonUp("Jump")) {
            if (velocity.y > 0) {
                velocity.y *= 0.5f;
            }
        }

        target_Velocity = move * max_Speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Physics_Controller_2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
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
            LevelEnd();
        }
    }

    void FixedUpdate() {
        PlayerMovement();
        PlayerRayCast();
    }

    void OnCollisionEnter2D(Collision2D collision_2D) {
        if (collision_2D.gameObject.tag == "Ground") {
            isGrounded = true;
        }

        if (collision_2D.gameObject.tag == "Level_End") {
            Level_End_Reached = true;
            Debug.Log(Level_End_Reached);
        }

        if (collision_2D.gameObject.tag == "Enemy_Wall") {
            Physics2D.IgnoreCollision(collision_2D.collider, GetComponent<Collider2D>());
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

    private void LevelEnd() { 
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}

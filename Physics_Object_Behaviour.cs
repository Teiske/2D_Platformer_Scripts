﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics_Object_Behaviour : MonoBehaviour {

    public float min_Ground_Normal_Y = .65f;
    public float gravity_Modifier = 1f;

    protected bool grounded;
    protected Vector2 ground_Normal;

    protected Vector2 targert_Velocity;
    protected Rigidbody2D Rigidbody2D;
    protected Vector2 velocity;
    protected ContactFilter2D ContactFilter2D;
    protected RaycastHit2D[] hit_Buffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hit_Buffer_List = new List<RaycastHit2D>(16);

    protected const float min_Move_Distance = 0.001f;
    protected const float shell_Radius = 0.01f;

    void OnEnable() {

        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start() {

        ContactFilter2D.useTriggers = false;
        ContactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        ContactFilter2D.useLayerMask = true;
    }

    void Update() {
        
    }

    void FixedUpdate() {

        velocity += gravity_Modifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targert_Velocity.x;
        grounded = false;
        Vector2 deltaPosition = velocity * Time.deltaTime;
        Vector2 move_Along_Ground = new Vector2(ground_Normal.y, -ground_Normal.x);
        Vector2 move = Vector2.up * deltaPosition.y;

        Movement(move, false);
        move = Vector2.up * deltaPosition.y;
        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement) {

        float distance = move.magnitude;

        if (distance > min_Move_Distance) {

            int count = Rigidbody2D.Cast(move, ContactFilter2D, hit_Buffer, distance + shell_Radius);
            hit_Buffer_List.Clear();
            for (int i = 0; i < count; i++) {
                hit_Buffer_List.Add(hit_Buffer[i]);
            }

            for (int i = 0; i < hit_Buffer_List.Count; i++) {

                Vector2 current_Normal = hit_Buffer_List[i].normal;
                if (current_Normal.y > min_Ground_Normal_Y) {

                    grounded = true;
                    if (yMovement) {

                        ground_Normal = current_Normal;
                        current_Normal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, current_Normal);
                if (projection < 0) {

                    velocity = velocity - projection * current_Normal;
                }

                float modified_Distance = hit_Buffer_List[i].distance - shell_Radius;
                distance = modified_Distance < distance ? modified_Distance : distance;
            }
        }

        Rigidbody2D.position = Rigidbody2D.position + move.normalized * distance;
    }
}

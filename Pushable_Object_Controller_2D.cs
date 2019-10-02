using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable_Object_Controller_2D : MonoBehaviour {

    public float gravity = 12;
    Physics_Controller_2D Controller_2D;
    Vector3 velocity;

    void Start() {
        Controller_2D = GetComponent<Physics_Controller_2D>();
    }

    void Update() {
        velocity += Vector3.down * gravity * Time.deltaTime;
        Controller_2D.Move(velocity * Time.deltaTime, false);
        if (Controller_2D.collisions.below) {
            velocity = Vector3.zero;
        }

    }

    //public Vector2 Push(Vector2 amount) {
    //    return Controller_2D.Move(velocity: amount, false);
    //}
}

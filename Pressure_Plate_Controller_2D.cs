using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_Plate_Controller_2D : Raycast_Controller_2D {

    public LayerMask weightMask;

    //public Physics_Controller_2D physics_Controller_2D;

    //void Start() {
        
    //}

    void Update() {
        UpdateRaycastOrigins();
        PlayerWeight();
    }

    void PlayerWeight() {
        float rayLength = skinWidth * 2;
        for (int i = 0; i < verticalRayCount; i++) {
            Vector2 rayOrigin = raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, weightMask);

            Debug.DrawRay(rayOrigin, Vector2.up * rayLength, Color.red);

            if (hit) {
                Debug.Log(weightMask + " is on pressure plate");
            }
            else if (!hit) {
                Debug.Log(weightMask + " is not on pressure plate");
            }
        }
    }
}

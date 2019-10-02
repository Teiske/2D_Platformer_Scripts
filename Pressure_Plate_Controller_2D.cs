using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_Plate_Controller_2D : MonoBehaviour {

    [SerializeField]
    private GameObject door;
    [SerializeField]
    private LayerMask weightMask;

    [SerializeField]
    private float time_Left;

    private float door_Move;
    private float door_Speed;

    private Vector2 closed_Position;

    void Start() {
        door_Move  = -4.5f;
        door_Speed = 3f;

        closed_Position = door.transform.position;
    }

    void Update() {
      
    }

    void OnTriggerEnter2D(Collider2D collider_2D) {
        StopCoroutine("MoveDoor");
        Vector2 end_Position = closed_Position + new Vector2(0, door_Move);
        StartCoroutine("MoveDoor", end_Position);
    }

    void OnTriggerExit2D(Collider2D collider_2D) {
        StopCoroutine("MoveDoor"); 
    }
    
    IEnumerator MoveDoor(Vector2 end_Position) {
        float time = 0f;
        Vector2 start_Postion = door.transform.position;
        while (time < 1f) {
            time += Time.deltaTime * door_Speed;
            door.transform.position = Vector2.Lerp(start_Postion, end_Position, time);
            yield return null;
        }
    }

    IEnumerator WaitTime() {
        yield return new WaitForSeconds(time_Left);
        StartCoroutine("MoveDoor", closed_Position);
    }
}

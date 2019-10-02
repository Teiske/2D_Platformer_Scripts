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
    private Vector2 start_Postion;

    private bool is_Openend = false;
    private bool run_Timer = false;

    void Start() {
        door_Move  = -4.5f;
        door_Speed = 3f;

        closed_Position = door.transform.position;
        //start_Postion = door.transform.position;
    }

    void Update() {
        if (run_Timer) {
            time_Left -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider_2D) {
        StopCoroutine("MoveDoor");
        Vector2 end_Position = closed_Position + new Vector2(0, door_Move);
        StartCoroutine("MoveDoor", end_Position);
    }

    void OnTriggerExit2D(Collider2D collider_2D) {
        StopCoroutine("MoveDoor");
        StartCoroutine("WaitTime");
        
        
    }

    void OpenDoor() {

    }
    
    IEnumerator MoveDoor(Vector2 end_Position /*float wait_Time*/) {
        float time = 0f;
        //wait_Time = time_Left;
        Vector2 start_Postion = door.transform.position;
        while (time < 1f) {
            time += Time.deltaTime * door_Speed;
            door.transform.position = Vector2.Lerp(start_Postion, end_Position, time);
            yield return null;
        }
    }

    IEnumerator WaitTime() {
        Debug.Log("safws");
        yield return new WaitForSeconds(time_Left);
        StartCoroutine("MoveDoor", closed_Position);
    }
}

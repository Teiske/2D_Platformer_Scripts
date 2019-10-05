using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Enemy_Controller_2D : Raycast_Controller_2D {

    private Rigidbody2D rigibody_2D;
    Raycast_Controller_2D raycast_Controller_2D;

    [SerializeField]
    private float enemy_Speed;
    [SerializeField]
    private float x_Move_Dir;
    [SerializeField]
    private float wait_Time;
    public bool move = true;

    void Start() {
        rigibody_2D.GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (move == true) {
            StartCoroutine("EnemyMove");
        }
        else if (move == false) {
            StartCoroutine("WaitForMove");
        }
    }

    public void FlipEnemy() {
        if (x_Move_Dir > 0) {
            x_Move_Dir = -1;
        }
        else {
            x_Move_Dir = 1;
        }
    }

    public IEnumerator EnemyMove() {
        float rayLength = 0.7f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(x_Move_Dir, 0));
        Debug.DrawRay(transform.position, Vector2.right * x_Move_Dir * rayLength, Color.cyan);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x_Move_Dir * enemy_Speed, 0); gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x_Move_Dir * enemy_Speed, 0);
        if (hit.distance < 0.7f) {
            FlipEnemy();
            if (hit.collider.tag == "Player") {
                Debug.Log("Enemy hits player");
                StartCoroutine("KillPlayer");
            }
        }
        yield return null;
    }

    public IEnumerator WaitForMove() {
        yield return new WaitForSeconds(wait_Time);
        move = true;
    }

    public IEnumerator KillPlayer() {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        yield return null;
    }
}

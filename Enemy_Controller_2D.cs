using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Enemy_Controller_2D : Raycast_Controller_2D {

    private Rigidbody2D rigibody_2D;
    
    [SerializeField]
    public float enemy_Speed;
    
    [SerializeField]
    public float x_Move_Dir;

    Raycast_Controller_2D raycast_Controller_2D;

    public bool move = true;

    void Start() {
        rigibody_2D.GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (move == true) {
            EnemyMove();
        }
        EnemyRayCast();
    }

    public void EnemyMove() {
        float rayLength = 0.7f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(x_Move_Dir, 0));
        Debug.DrawRay(transform.position, Vector2.right * x_Move_Dir * rayLength, Color.cyan);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x_Move_Dir * enemy_Speed, 0); gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x_Move_Dir * enemy_Speed, 0);
        if (hit.distance < 0.7f) {
            FlipEnemy();
            if (hit.collider.tag == "Player") {
                Debug.Log("Enemy hits player"); StartCoroutine("KillPlayer");
            }
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

    public IEnumerator KillPlayer() {
        SceneManager.LoadScene("Test_Scene");
        yield return null;
    }
}

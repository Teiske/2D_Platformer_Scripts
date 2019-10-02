using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player_Life_Controller_2D : MonoBehaviour {

    private int health;
    private bool has_Died;

    void Start() {
        has_Died = false;
    }

    void Update() {
        if (gameObject.transform.position.y < -7) {
            has_Died = true;
            if (has_Died) {
                StartCoroutine("Die");
            }
        }
    }

    public IEnumerator Die() {
        Debug.Log("Player dies");
        SceneManager.LoadScene("Test_Scene");
        yield return null;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKiller : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
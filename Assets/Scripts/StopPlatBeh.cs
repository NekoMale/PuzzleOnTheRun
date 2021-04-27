using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlatBeh : MonoBehaviour
{
    [SerializeField] KeyCode RestartKey;
    [SerializeField] Transform _yPosition;
    private bool performUpdate = false;

    private Movement playerMov = null;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        if (Input.GetKeyDown(RestartKey) && playerMov != null) {
            playerMov.ResumeMovement();
            playerMov = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            rb.bodyType = RigidbodyType2D.Static;
        }
        else if(collision.gameObject.CompareTag("Player")) {
            playerMov = collision.gameObject.GetComponent<Movement>();
            playerMov.transform.position = new Vector3(playerMov.transform.position.x + 0.1f, _yPosition.position.y);
            playerMov.Stop();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (playerMov == null) return;
        playerMov.ResumeMovement();
        playerMov = null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBostBeh : MonoBehaviour {
    [SerializeField] float TotalTime = 2f;
    [SerializeField] float Boost;
    [SerializeField] Transform _yPosition;
    private Movement playerMov;
    private Rigidbody2D rb;


    private float Timer = 0;
    private bool performUpdate = false;
    void Update() {
        if (playerMov == null) return;
        Timer += Time.deltaTime;
        if (Timer < TotalTime) return;
        playerMov.ResetMovement();
        playerMov = null;
        Timer = 0;
    }
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            rb.bodyType = RigidbodyType2D.Static;
        }
        else if (collision.gameObject.CompareTag("Player")) {
            playerMov = collision.gameObject.GetComponent<Movement>();
            playerMov.transform.position = new Vector3(playerMov.transform.position.x + 0.1f, _yPosition.position.y);
            playerMov.MultiplyMoveSpeed(Boost);
        }
    }
}
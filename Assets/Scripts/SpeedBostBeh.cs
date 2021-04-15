using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBostBeh : MonoBehaviour
{
    [SerializeField] float TotalTime = 2f;
    [SerializeField] float Boost;
    private Movement moveSys;
    private Rigidbody2D rb;


    private float Timer = 0;
    private bool performUpdate = false;
    void Update()
    {
        if (!performUpdate) return;
        Timer += Time.deltaTime;
        if (Timer >= TotalTime)
        {
            moveSys.ResumeMovement();
            Timer = 0;
            performUpdate = false;
        }
    }
    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (moveSys == null)
            moveSys = collision.gameObject.GetComponent<Movement>();
        performUpdate = true;
        moveSys.MultiplyMoveSpeed(Boost);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
        else if (collision.gameObject.CompareTag("Player") && rb.bodyType != RigidbodyType2D.Static)
        {
            Debug.Log("morto!!!!");
        }
    }
}

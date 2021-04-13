using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBostBeh : MonoBehaviour
{
    [SerializeField] float TotalTime = 2f;
    [SerializeField] float Boost;
    private Movement moveSys;

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (moveSys == null)
            moveSys = collision.gameObject.GetComponent<Movement>();
        performUpdate = true;
        moveSys.MultiplyMoveSpeed(Boost);
    }
}

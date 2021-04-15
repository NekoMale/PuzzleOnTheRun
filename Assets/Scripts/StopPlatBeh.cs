using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlatBeh : MonoBehaviour
{
    [SerializeField] KeyCode RestartKey;
    private Movement moveSys;
    private bool performUpdate = false;
    private bool restartNeeded = false;

    private void Update()
    {
        if (!performUpdate) return;
        if (Input.GetKeyDown(RestartKey)) { restartNeeded = true; performUpdate = false; }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Movement>().StopMovement();
    }
    /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.position.y <= this.transform.position.y) { Debug.Log("mammeta"); return; }
        if (moveSys == null)
            moveSys = collision.gameObject.GetComponent<Movement>();
        
        moveSys.StopMovement();
    }
   
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (performUpdate) return;
        else performUpdate = true;
        if (restartNeeded) { moveSys.ResumeMovement(); restartNeeded = false;  performUpdate = false; }
     }
    */
}

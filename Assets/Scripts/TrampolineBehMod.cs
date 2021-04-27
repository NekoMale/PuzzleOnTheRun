using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineBehMod : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] string TriggerName;
    [SerializeField] float JumpModifier;
    [SerializeField] float _movementModifier;
    Rigidbody2D rb;
    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Anim.SetTrigger(TriggerName);
        Movement mov = collision.gameObject.GetComponent<Movement>();
        mov.ChangeJumpForce(JumpModifier);
        mov.ChangeMoveSpeed(_movementModifier);
        mov.Jump();
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        Anim.ResetTrigger(TriggerName);
        collision.gameObject.GetComponent<Movement>().ResetJump();
    }
   
}

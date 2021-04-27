using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineBeh : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] string TriggerName;
    [SerializeField] float JumpModifier;
    [SerializeField] float _movementModifier;
    [SerializeField] Transform _yPosition;
    Rigidbody2D rb;

    private void Start() {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            rb.bodyType = RigidbodyType2D.Static;
        }
        else if (collision.gameObject.CompareTag("Player")) {
            Anim.SetTrigger(TriggerName);
            Movement mov = collision.gameObject.GetComponent<Movement>();
            mov.transform.position = new Vector3(mov.transform.position.x + 0.15f, _yPosition.position.y);
            mov.ChangeMoveSpeed(_movementModifier);
            mov.ChangeJumpForce(JumpModifier);
            mov.Jump();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Player")) return;
        Anim.ResetTrigger(TriggerName);
        Movement mov = collision.gameObject.GetComponent<Movement>();
        mov.ResetMovement();
        mov.ResetJump();
    }
}

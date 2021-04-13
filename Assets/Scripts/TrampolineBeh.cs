using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineBeh : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] string TriggerName;
    [SerializeField] float JumpModifier;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Anim.SetTrigger(TriggerName);
        Movement mov = collision.gameObject.GetComponent<Movement>();
        mov.ChangeJumpForce(JumpModifier);
        mov.Jump();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Anim.ResetTrigger(TriggerName);
        collision.gameObject.GetComponent<Movement>().ResetJump();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HorizonthalSaw : MonoBehaviour
{
    Rigidbody2D sawRigidbody = null;
    SpriteRenderer sawSpriteRenderer;
    private Animator anim;
    Transform childToRemove;

    [Header("Enter here the name of the game scene where this trap is located")]
    public string GameSceneName;
    [Header("Blade move limits")]
    public Transform SxSawLimit;
    public Transform DxSawLimit;
    [Header("Blade controller")]
    public float SawSpeed;
    public float SawSpeedRotation = 1;
    public bool FlipBladeOnDirectionChange = true;

    // Start is called before the first frame update
    void Start()
    {
        sawRigidbody = GetComponent<Rigidbody2D>();
        sawSpriteRenderer = GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        sawRigidbody.velocity = new Vector2(SawSpeed, 0);

        //Remove all children in Saw to keep them from moving with saw
        for (int i = this.transform.childCount -1; i >= 0; i--)
        {
            childToRemove = this.transform.GetChild(i);
            childToRemove.transform.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.speed = SawSpeedRotation;

        if (this.transform.position.x >= DxSawLimit.position.x)
        {
            sawRigidbody.velocity = new Vector2(-SawSpeed, 0);
            if (FlipBladeOnDirectionChange)
            {
                sawSpriteRenderer.flipX = true;
            }
        }
        if (this.transform.position.x <= SxSawLimit.position.x)
        {
            sawRigidbody.velocity = new Vector2(SawSpeed, 0);
            if (FlipBladeOnDirectionChange)
            {
                sawSpriteRenderer.flipX = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}

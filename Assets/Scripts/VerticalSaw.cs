using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VerticalSaw : MonoBehaviour
{
    Rigidbody2D sawRigidbody = null;
    SpriteRenderer sawSpriteRenderer;
    private Animator anim;
    Transform childToRemove;

    string GameSceneName;
    [Header("Blade move limits")]
    public Transform UpSawLimit;
    public Transform DownSawLimit;
    [Header("Blade controller")]
    public float SawSpeed;
    public float SawSpeedRotation = 1;
    public bool FlipBladeOnDirectionChange = true;

    // Start is called before the first frame update
    void Start()
    {
        GameSceneName = SceneManager.GetActiveScene().name;
        sawRigidbody = GetComponent<Rigidbody2D>();
        sawSpriteRenderer = GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        sawRigidbody.velocity = new Vector2(0, SawSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        anim.speed = SawSpeedRotation;

        if (this.transform.position.y <= DownSawLimit.position.y)
        {
            sawRigidbody.velocity = new Vector2(0, SawSpeed);
            if (FlipBladeOnDirectionChange)
            {
                sawSpriteRenderer.flipX = true;
            }
        }
        if (this.transform.position.y >= UpSawLimit.position.y)
        {
            sawRigidbody.velocity = new Vector2(0, -SawSpeed);
            if (FlipBladeOnDirectionChange)
            {
                sawSpriteRenderer.flipX = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}

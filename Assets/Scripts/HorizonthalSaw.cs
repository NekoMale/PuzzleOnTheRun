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

    public GameObject SawChainPrefab;
    string GameSceneName;
    [Header("Blade move limits")]
    public Transform SxSawLimit;
    public Transform DxSawLimit;
    [Header("Blade controller")]
    public float SawSpeed;
    public float SawSpeedRotation = 1;
    public bool FlipBladeOnDirectionChange = true;
    public int SawChainNumber;
    // Start is called before the first frame update
    void Start()
    {
        GameSceneName = SceneManager.GetActiveScene().name;
        sawRigidbody = GetComponent<Rigidbody2D>();
        sawSpriteRenderer = GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        sawRigidbody.velocity = new Vector2(SawSpeed, 0);
        //HorizonthalSawChainInstantiator();
        
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
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }

    //Da Rivedere
    /*
    void HorizonthalSawChainInstantiator()
    {
        float distance = Vector2.Distance(SxSawLimit.position, DxSawLimit.position);
        float distanceForSpawnChain = distance / SawChainNumber;
        Vector2 SpawnPos = new Vector2(0, 0);
        for (int i = 0; i < SawChainNumber; i++)
        {
            SpawnPos.x += distanceForSpawnChain;
            GameObject ring = Instantiate(SawChainPrefab);
            ring.transform.parent = SxSawLimit.transform;
            ring.transform.localPosition = new Vector2(SpawnPos.x, 0);

        }
    }
    */
}

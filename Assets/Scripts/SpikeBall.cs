using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeBall : MonoBehaviour
{
    [Header("Enter here the name of the game scene where this trap is located")]
    public string GameSceneName;
    [Header("Enter here the RigidBody of anchor point")]
    public Rigidbody2D AnchorRigidBody;
    [Header("Pendulum movement controller")]
    public float moveSpeed = 50;
    public float leftAngle = -0.4f;
    public float rightAngle = 0.4f;
    [Header("Chain instantiator controller")]
    public bool InstantiateChain = true;
    public Transform AnchorPoint;
    public Transform SpikeHead;
    public int RingOfChainNumber;
    public GameObject ChainPrefab;

    bool movingClockwise;
    // Start is called before the first frame update
    void Start()
    {
        //AnchorRigidBody = GetComponent<Rigidbody2D>();
        movingClockwise = true;
        if (InstantiateChain)
        {
            ChainInstantiator();
        }
    }

    // Update is called once per frame
    void Update()
    {

        Move();
    }

    public void ChangeMoveDir()
    {
        if (transform.rotation.z > rightAngle)
        {
            movingClockwise = false;
        }
        if (transform.rotation.z < leftAngle)
        {
            movingClockwise = true;
        }

    }

    public void Move()
    {
        ChangeMoveDir();

        if (movingClockwise)
        {
            AnchorRigidBody.angularVelocity = moveSpeed;
        }

        if (!movingClockwise)
        {
            AnchorRigidBody.angularVelocity = -1 * moveSpeed;
        }
    }

    void ChainInstantiator()
    {
        float distance = Vector2.Distance(AnchorPoint.position, SpikeHead.position);
        float distanceForSpawnChain = distance / RingOfChainNumber;
        Vector2 SpawnPos = new Vector2(0, 0);
        for (int i = 0; i < RingOfChainNumber; i++)
        {
            SpawnPos.y -= distanceForSpawnChain;
            GameObject ring = Instantiate(ChainPrefab);
            ring.transform.parent = AnchorPoint.transform;
            ring.transform.localPosition = new Vector2(0, SpawnPos.y);

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

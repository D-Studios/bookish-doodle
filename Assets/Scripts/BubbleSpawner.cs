using UnityEngine;
using System.Collections;

public class BubbleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject bubblePrefab;
    public static BubbleSpawner bubbleSpawner;
    public float spawnInterval = 2f;
    public int maxBubbles = 20;
    public Vector2 spawnAreaMin = new Vector2(-8, -4);
    public Vector2 spawnAreaMax = new Vector2(8, 4);

    [Header("Bubble Properties")]
    public float minBubbleSize = 0.5f;
    public float maxBubbleSize = 1.5f;
    public float playerGrowthAmount = 0.1f;
    
    private int currentBubbles = 0;

    private void Awake()
    {
        bubbleSpawner = this;
    }

    void Start()
    {
        StartCoroutine(SpawnBubbles());
    }

    IEnumerator SpawnBubbles()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnInterval);
            
            if(currentBubbles < maxBubbles)
            {
                Vector2 spawnPos = new Vector2(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y)
                );

                GameObject bubble = Instantiate(bubblePrefab, spawnPos, Quaternion.identity);
                bubble.tag = "Bubble";
                
                // Set random size
                float size = Random.Range(minBubbleSize, maxBubbleSize);
                bubble.transform.localScale = new Vector3(size, size, 1f);
                
                // Add physics components
                Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>() ?? bubble.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;
                rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

                CircleCollider2D theCollider = bubble.GetComponent<CircleCollider2D>() ?? bubble.AddComponent<CircleCollider2D>();
                theCollider.isTrigger = true;

                currentBubbles++;
                //BubbleBehavior behavior = bubble.AddComponent<BubbleBehavior>();
                //behavior.Initialize(this, playerGrowthAmount);
            }
        }
    }

    public void BubbleDestroyed()
    {
        currentBubbles--;
    }
}

//public class BubbleBehavior : MonoBehaviour
//{
//    private BubbleSpawner spawner = BubbleSpawner.bubbleSpawner;
//    private float growthAmount;

//    public void Initialize(BubbleSpawner spawnerRef, float growth)
//    {
//        spawner = spawnerRef;
//        growthAmount = growth;
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if(other.CompareTag("Player"))
//        {
//            // Grow the player
//            PlayerMovement player = other.GetComponent<PlayerMovement>();
//            if(player != null)
//            {
//                player.GrowPlayer(growthAmount);
//            }
            
//            // Notify spawner and destroy bubble
//            if(spawner != null)
//            {
//                spawner.BubbleDestroyed();
//            }
//            Destroy(gameObject);
//        }
//    }
//}
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject obstaclePrefab;
    public GameObject enemyPrefab;
    public GameObject healthPackPrefab;
    
    [Header("Spawn Settings")]
    public float baseObstacleSpawnRate = 3f; 
    public float baseItemSpawnRate = 2f; 
    
    private float nextObstacleTime;
    private float nextItemTime;

    void Start()
    {
        nextObstacleTime = Time.time + baseObstacleSpawnRate;
        nextItemTime = Time.time + baseItemSpawnRate;
    }

    void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.isGameStarted || GameManager.Instance.isGameOver) 
        {
            // Keep pushing the spawn time forward while game holds so they don't instantly pop out
            if (GameManager.Instance != null && !GameManager.Instance.isGameStarted)
            {
                nextObstacleTime = Time.time + baseObstacleSpawnRate;
                nextItemTime = Time.time + baseItemSpawnRate;
            }
            return;
        }
        
        float speedMultiplier = GameManager.Instance.globalSpeed / 5f; 
        
        if (Time.time >= nextObstacleTime)
        {
            SpawnObstacle();
            nextObstacleTime = Time.time + (baseObstacleSpawnRate / speedMultiplier);
        }
        
        if (Time.time >= nextItemTime)
        {
            SpawnItem();
            nextItemTime = Time.time + (baseItemSpawnRate / speedMultiplier);
        }
    }
    
    void SpawnObstacle()
    {
        if (obstaclePrefab == null) return;

        float randomY = Random.Range(-2.5f, 2.5f);
        Vector3 spawnPos = new Vector3(12f, randomY, -1f); 
        
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }
    
    void SpawnItem()
    {
        float randomY = Random.Range(-4f, 4f);
        Vector3 spawnPos = new Vector3(12f, randomY, -1f); // Spawns slightly in front to prevent visual overlap 
        
        // 20% chance HealthPack, 80% chance Enemy (Pure random logic)
        float randomVal = Random.value;
        GameObject prefab = (randomVal > 0.8f && healthPackPrefab != null) ? healthPackPrefab : enemyPrefab;
        
        if (prefab != null)
        {
            // Force Z-position to -1 for 100% collision reliability!
            spawnPos.z = -1f;
            
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }
}

using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject platformPrefab;
    public GameObject trapPrefab;
    public GameObject bouncyPrefab;
    public GameObject enemyPrefab;

    [Header("Generation Limits")]
    public int platformCount = 10;
    public float levelWidth = 3f;

    [Header("Base Difficulty (Start of Game)")]
    public float minY = 0.5f;
    public float maxY = 1.5f;
    [Range(0f, 1f)] public float trapChance = 0.05f;   // 5% chance
    [Range(0f, 1f)] public float bouncyChance = 0.10f; // 10% chance
    [Range(0f, 1f)] public float enemyChance = 0.00f;  // 0% chance at start

    [Header("Max Difficulty (Endgame)")]
    public float heightForMaxDifficulty = 300f; // The score where the game stops getting harder
    public float absoluteMaxGap = 3.2f; // CRITICAL: Test this to ensure your crab can actually jump this high!
    [Range(0f, 1f)] public float maxTrapChance = 0.25f; // 25% chance
    [Range(0f, 1f)] public float maxEnemyChance = 0.15f; // 15% chance

    private float spawnY;

    void Start()
    {
        spawnY = transform.position.y;

        for (int i = 0; i < platformCount; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        if (Camera.main.transform.position.y + 10f > spawnY)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        // 1. Calculate Difficulty Math
        float difficultyPercent = Mathf.Clamp01(spawnY / heightForMaxDifficulty);

        float currentMinY = Mathf.Lerp(minY, maxY, difficultyPercent);
        float currentMaxY = Mathf.Lerp(maxY, absoluteMaxGap, difficultyPercent);

        float currentTrapChance = Mathf.Lerp(trapChance, maxTrapChance, difficultyPercent);
        float currentEnemyChance = Mathf.Lerp(enemyChance, maxEnemyChance, difficultyPercent);

        spawnY += Random.Range(currentMinY, currentMaxY);
        Vector3 spawnPosition = new(Random.Range(-levelWidth, levelWidth), spawnY, 0f);

        GameObject prefabToSpawn = platformPrefab;
        float platformRoll = Random.value;

        if (platformRoll < currentTrapChance)
        {
            prefabToSpawn = trapPrefab;
        }
        else if (platformRoll < currentTrapChance + bouncyChance)
        {
            prefabToSpawn = bouncyPrefab;
        }

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        if (Random.value < currentEnemyChance)
        {
            Vector3 enemySpawnPos = new(Random.Range(-levelWidth, levelWidth), spawnY + 1.5f, 0f);

            Instantiate(enemyPrefab, enemySpawnPos, Quaternion.identity);
        }
    }
}
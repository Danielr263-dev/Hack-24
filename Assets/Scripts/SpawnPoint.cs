using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab; // The enemy prefab to spawn

    [SerializeField]
    private float _minimumSpawnTime = 1f; // Minimum time between spawns

    [SerializeField]
    private float _maximumSpawnTime = 5f; // Maximum time between spawns

    private float _timeUntilSpawn; // Time left until the next spawn

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        SetTimeUntilSpawn(); // Set the initial spawn time delay
    }

    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0)
        {
            SpawnEnemyOutsideCamera();
            SetTimeUntilSpawn(); // Reset the spawn timer
        }
    }

    // Spawns an enemy at a random position outside the camera view
    private void SpawnEnemyOutsideCamera()
    {
        // Get the camera bounds in world space
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Set positions slightly beyond the camera's edges
        float bufferDistance = 2f; // Extra distance to spawn outside the camera bounds

        // Choose a random edge: 0 = left, 1 = right, 2 = top, 3 = bottom
        int randomEdge = Random.Range(0, 4);
        Vector2 spawnPosition = Vector2.zero;

        switch (randomEdge)
        {
            case 0: // Left edge
                spawnPosition = new Vector2(screenBottomLeft.x - bufferDistance, Random.Range(screenBottomLeft.y, screenTopRight.y));
                break;
            case 1: // Right edge
                spawnPosition = new Vector2(screenTopRight.x + bufferDistance, Random.Range(screenBottomLeft.y, screenTopRight.y));
                break;
            case 2: // Top edge
                spawnPosition = new Vector2(Random.Range(screenBottomLeft.x, screenTopRight.x), screenTopRight.y + bufferDistance);
                break;
            case 3: // Bottom edge
                spawnPosition = new Vector2(Random.Range(screenBottomLeft.x, screenTopRight.x), screenBottomLeft.y - bufferDistance);
                break;
        }

        // Instantiate the enemy at the chosen spawn position
        Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
    }

    // Sets the time until the next spawn using a random value between min and max spawn times
    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}

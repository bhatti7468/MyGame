using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnRate = 2f;
    public float laneDistance = 2f;
    public float spawnY = 4f; // સ્ક્રીન ની અંદર દેખાય તે માટે 4f રાખ્યું છે

    private float nextSpawnTime = 0f;
    private float[] laneXPositions;

    void Start()
    {
        laneXPositions = new float[] { -laneDistance, 0f, laneDistance };
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (Time.time > nextSpawnTime)
        {
            SpawnCoin();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnCoin()
    {
        if (coinPrefab == null)
        {
            Debug.LogError("⚠️ Coin Prefab Assign નથી કર્યું!");
            return;
        }

        int randomLane = Random.Range(0, 3);
        Vector3 spawnPosition = new Vector3(laneXPositions[randomLane], spawnY, 0f);
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

        Debug.Log("🪙 Coin Spawn થયું Lane: " + randomLane);
    }
}
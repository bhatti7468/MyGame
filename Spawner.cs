using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 1.5f;
    public float laneDistance = 2f; // Player Movement ના laneDistance સાથે મેચ રાખવું
    public float spawnY = 6f;       // Screen ની ઉપરથી સ્પોન કરવા માટે Y પોઝિશન

    private float nextSpawnTime = 0f;
    private float[] laneXPositions;

    void Start()
    {
        // 3 રસ્તાઓ માટેના X સ્થાનો (-2, 0, 2)
        laneXPositions = new float[] { -laneDistance, 0f, laneDistance };
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (Time.time > nextSpawnTime)
        {
            SpawnObstacles();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnObstacles()
    {
        // રેન્ડમલી 1 અથવા 2 ઓબ્સ્ટેકલ જ પસંદ થશે (3 ક્યારેય નહીં થાય)
        int obstacleCount = Random.Range(1, 3);

        List<int> availableLanes = new List<int> { 0, 1, 2 };

        for (int i = 0; i < obstacleCount; i++)
        {
            // ઉપલબ્ધ રસ્તાઓમાંથી એક રેન્ડમ રસ્તો પસંદ કરો
            int randomIndex = Random.Range(0, availableLanes.Count);
            int chosenLane = availableLanes[randomIndex];

            // ડુપ્લિકેટ સ્પોન ન થાય માટે લિસ્ટમાંથી દૂર કરો
            availableLanes.RemoveAt(randomIndex);

            // Obstacle સ્પોન કરો
            Vector3 spawnPosition = new Vector3(laneXPositions[chosenLane], spawnY, 0f);
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
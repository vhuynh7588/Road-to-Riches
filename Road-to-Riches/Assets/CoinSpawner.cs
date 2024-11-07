using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;  // Reference to the coin prefab
    public int maxCoins = 5;       // Max number of coins on the map
    public Vector2 spawnAreaMin;   // Bottom-left corner of the spawn area
    public Vector2 spawnAreaMax;   // Top-right corner of the spawn area

    private List<GameObject> activeCoins = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxCoins; i++)
        {
            SpawnCoin();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activeCoins.Count < maxCoins)
        {
            SpawnCoin();
        }
    }

    void SpawnCoin()
    {
        // Randomly pick a position within the defined spawn area
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(x, y);

        // Instantiate a new coin at the random position
        GameObject newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

        // Add the new coin to the list of active coins
        activeCoins.Add(newCoin);

        // Listen for the coin's destruction
        newCoin.GetComponent<SC_2DCoin>().OnCoinCollected += HandleCoinCollected;
    }

    void HandleCoinCollected(GameObject coin)
    {
        // Remove the coin from the active list when it's collected
        activeCoins.Remove(coin);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject collectEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Make sure the car has the tag "Player"
        {
            // Play collect sound
            // Show collection effect (e.g., particle effect)
            if (collectEffect != null)
                Instantiate(collectEffect, transform.position, Quaternion.identity);

            // Optional: Update score
            GameManager.instance.AddScore(1);  // assuming a GameManager class handles the score

            // Destroy coin
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

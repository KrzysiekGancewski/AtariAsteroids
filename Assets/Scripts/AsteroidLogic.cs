using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class AsteroidLogic : MonoBehaviour {

    private AsteroidWaveManager asteroidWaveManager;
    private AsteroidWaveManager.AsteroidSize asteroidSize;
    public AsteroidWaveManager.AsteroidSize AsteroidSize { 
        get { return asteroidSize; }
        set { asteroidSize = value; }
    }

    private void Start() {
        asteroidWaveManager = GetComponentInParent<AsteroidWaveManager>();
        if (asteroidWaveManager == null) {
            Debug.Log("AsteroidLogic || asteroidWaveManager == null");
            Destroy(gameObject);
        }
    }

    public void DestroyWithPoints() {
        asteroidWaveManager.AddPoints(AsteroidSize);
        asteroidWaveManager.InstantiateFX(AsteroidSize, transform.position);

        //Spawn new asteroids
        for (int i = 0; i < GameManager.instance.NumberOfAsteroidsToSpawnAfterDestroy; ++i) {
            if (AsteroidSize == AsteroidWaveManager.AsteroidSize.Big) {
                asteroidWaveManager.CreateAsteroid(AsteroidWaveManager.AsteroidSize.Medium, transform.position, GetComponent<Rigidbody2D>().velocity);
            } else if (AsteroidSize == AsteroidWaveManager.AsteroidSize.Medium) {
                asteroidWaveManager.CreateAsteroid(AsteroidWaveManager.AsteroidSize.Small, transform.position, GetComponent<Rigidbody2D>().velocity);
            }
        }

        Destroy(gameObject);
    }
}

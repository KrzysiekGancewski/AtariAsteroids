using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidWaveManager : MonoBehaviour {

    [SerializeField]
    private ScoreManager scoreManager;
    [SerializeField]
    private GameObject[] asteroidPrefabs;
    [SerializeField]
    private GameObject ExplosionSoundEffect;
    [SerializeField]
    private GameObject ExplosionEffect;

    private enum SpawnSide { 
        right,
        bottom,
        left,
        top
    }

    public enum AsteroidSize {
        Big,
        Medium,
        Small
    }

    void Start () {
        StartCoroutine("SpawnNextWave");
    }

    private IEnumerator SpawnNextWave() {
        //Wait few seconds before spawning wave of asteroids
        yield return new WaitForSeconds(GameManager.instance.RespawnTime);

        while (true) { 
            for (int i = 0; i < GameManager.instance.NumberOfAsteroidsInSingleWave; ++i) {
                CreateBigAsteroid();
            }

            yield return new WaitForSeconds(GameManager.instance.AsteroidWaveSpawnTime);
        }
    }

    public void DestroyAllAsteroids(bool startNextWaveAfterDestroy) {
        StopCoroutine("SpawnNextWave");
        foreach (var asteroid in GetComponentsInChildren<AsteroidLogic>()) {
            Destroy(asteroid.gameObject);
        }

        if (startNextWaveAfterDestroy) {
            StartCoroutine("SpawnNextWave");
        }
    }

    public void AddPoints(AsteroidSize asteroidSize) {
        if (asteroidSize == AsteroidSize.Big) {
            scoreManager.CurrentScore += GameManager.instance.PointsForBigRock;
        } else if (asteroidSize == AsteroidSize.Medium) { 
            scoreManager.CurrentScore += GameManager.instance.PointsForMediumRock;
        } else if (asteroidSize == AsteroidSize.Small) { 
            scoreManager.CurrentScore += GameManager.instance.PointsForSmallRock;
        }
    }

    public void InstantiateFX(AsteroidSize asteroidSize, Vector2 position) {
        GameObject explosionffect = Instantiate(ExplosionEffect, position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        Instantiate(ExplosionSoundEffect);

        if (asteroidSize == AsteroidSize.Medium) {
            explosionffect.transform.localScale = explosionffect.transform.localScale / 2;
        } else if (asteroidSize == AsteroidSize.Small) {
            explosionffect.transform.localScale = explosionffect.transform.localScale / 3;
        }
    }

    public void CreateAsteroid(AsteroidSize asteroidSize, Vector2 position, Vector2 parentVelocity) {
        GameObject asteroidToInstantiate = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
        GameObject go = Instantiate(asteroidToInstantiate, position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));

        if (asteroidSize == AsteroidSize.Medium) {
            go.transform.localScale = go.transform.localScale / 2;
        } else if (asteroidSize == AsteroidSize.Small) {
            go.transform.localScale = go.transform.localScale / 3;
        }

        //Setting asteroid as a child is an alternative for storing them in List or using Find function in future to delete all exisiting instances
        go.transform.SetParent(transform);
        Rigidbody2D asteroidRigidbody = go.GetComponent<Rigidbody2D>();
        if (asteroidRigidbody == null) {
            Debug.Log("asteroid's rigibody == null");
            Destroy(go);
            return;
        }

        go.GetComponent<AsteroidLogic>().AsteroidSize = asteroidSize;
        
        asteroidRigidbody.velocity = Random.Range(0.8f, 1.2f) * parentVelocity;
        asteroidRigidbody.AddForce(go.transform.right * Random.Range(-1f, 1f), ForceMode2D.Impulse);
        asteroidRigidbody.AddTorque(Random.Range(-1f, 1f) * GameManager.instance.AsteroidMaxTurnSpeed, ForceMode2D.Impulse);
    }

    private void CreateBigAsteroid() {
        GameObject asteroidToInstantiate = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        SpriteRenderer asteroidToInstantiateSR = asteroidToInstantiate.GetComponent<SpriteRenderer>();

        Vector2 bottomLeftScreenCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        Vector2 topRightScreenCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));

        float randomYWithingScreenBoundaries = Random.Range(bottomLeftScreenCorner.y + asteroidToInstantiateSR.size.y, topRightScreenCorner.y - asteroidToInstantiateSR.size.y);
        float randomXWithingScreenBoundaries = Random.Range(bottomLeftScreenCorner.x + asteroidToInstantiateSR.size.x, topRightScreenCorner.x - asteroidToInstantiateSR.size.x);
        float yBeyondTopScreenBoundary = topRightScreenCorner.y + asteroidToInstantiateSR.size.y;
        float xBeyondRightScreenBoundary = topRightScreenCorner.x + asteroidToInstantiateSR.size.x;
        
        Vector2 asteroidSpawnPoint = Vector2.zero;
        SpawnSide randomSpawnSide = (SpawnSide)Random.Range(0, System.Enum.GetValues(typeof(SpawnSide)).Length);
        if (randomSpawnSide == SpawnSide.right) {
            asteroidSpawnPoint = new Vector2(xBeyondRightScreenBoundary, randomYWithingScreenBoundaries);
        } else if (randomSpawnSide == SpawnSide.bottom) {
            asteroidSpawnPoint = new Vector2(randomXWithingScreenBoundaries, -yBeyondTopScreenBoundary);
        } else if (randomSpawnSide == SpawnSide.left) {
            asteroidSpawnPoint = new Vector2(-xBeyondRightScreenBoundary, randomYWithingScreenBoundaries);
        } else if (randomSpawnSide == SpawnSide.top) {
            asteroidSpawnPoint = new Vector2(randomXWithingScreenBoundaries, yBeyondTopScreenBoundary);
        }

        //Instantiating asteroid with random rotation
        GameObject go = Instantiate(asteroidToInstantiate, asteroidSpawnPoint, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        //Setting asteroid as a child is an alternative for storing them in List or using Find function in future to delete all exisiting instances
        go.transform.SetParent(transform);
        Rigidbody2D asteroidRigidbody = go.GetComponent<Rigidbody2D>();
        if (asteroidRigidbody == null) {
            Debug.Log("asteroid's rigibody == null");
            Destroy(go);
            return;
        }

        go.GetComponent<AsteroidLogic>().AsteroidSize = AsteroidSize.Big;

        //Direction of applied force doesn't matter because 'ScreenCornerDetection' script will take care of keeping object in bounds anyway
        asteroidRigidbody.AddForce(go.transform.up * Random.Range(-1f, 1f) * GameManager.instance.AsteroidMaxSpeed, ForceMode2D.Impulse);
        asteroidRigidbody.AddTorque(Random.Range(-1f, 1f) * GameManager.instance.AsteroidMaxTurnSpeed, ForceMode2D.Impulse);
    }
}

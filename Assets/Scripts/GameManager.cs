using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [Header("General settings")]
    [SerializeField, GetSet("RespawnTime")]
    private float respawnTime;
    [SerializeField, GetSet("NumberOfLives")]
    private int numberOfLives;

    [Header("Spaceship settings")]
    [SerializeField, GetSet("SpaceshipMaxSpeed")]
    private float spaceshipMaxSpeed;
    [SerializeField, GetSet("SpaceshipTurnSpeed")]
    private float spaceshipTurnSpeed;

    [Header("Bullet settings")]
    [SerializeField, GetSet("BulletSpeed")]
    private float bulletSpeed;
    [SerializeField, GetSet("BulletDestroyTime")]
    private float bulletDestroyTime;

    [Header("Asteroid settings")]
    [SerializeField, GetSet("AsteroidMaxSpeed")]
    private float asteroidMaxSpeed;
    [SerializeField, GetSet("AsteroidMaxTurnSpeed")]
    private float asteroidMaxTurnSpeed;
    [SerializeField, GetSet("AsteroidWaveSpawnTime")]
    private float asteroidWaveSpawnTime;
    [SerializeField, GetSet("NumberOfAsteroidsInSingleWave")]
    private int numberOfAsteroidsInSingleWave;
    [SerializeField, GetSet("NumberOfAsteroidsToSpawnAfterDestroy")]
    private int numberOfAsteroidsToSpawnAfterDestroy;

    [Header("Points settings")]
    [SerializeField, GetSet("PointsForSmallRock")]
    private int pointsForSmallRock;
    [SerializeField, GetSet("PointsForMediumRock")]
    private int pointsForMediumRock;
    [SerializeField, GetSet("PointsForBigRock")]
    private int pointsForBigRock;

    public int NumberOfLives {
        get { return numberOfLives; }
        private set { numberOfLives = ((int)value < 0) ? 0 : (int)value; }
    }
    public float RespawnTime {
        get { return respawnTime; }
        private set { respawnTime = (value < 0) ? 0 : value; }
    }
    public float SpaceshipMaxSpeed {
        get { return spaceshipMaxSpeed; }
        private set { spaceshipMaxSpeed = (value < 0) ? 0 : value; }
    }
    public float SpaceshipTurnSpeed {
        get { return spaceshipTurnSpeed; }
        private set { spaceshipTurnSpeed = (value < 0) ? 0 : value; }
    }
    public float BulletSpeed {
        get { return bulletSpeed; }
        private set { bulletSpeed = (value < 0) ? 0 : value; }
    }
    public float BulletDestroyTime {
        get { return bulletDestroyTime; }
        private set { bulletDestroyTime = (value < 0) ? 0 : value; }
    }
    public float AsteroidMaxSpeed {
        get { return asteroidMaxSpeed; }
        private set { asteroidMaxSpeed = (value < 0) ? 0 : value; }
    }
    public float AsteroidMaxTurnSpeed {
        get { return asteroidMaxTurnSpeed; }
        private set { asteroidMaxTurnSpeed = (value < 0) ? 0 : value; }
    }
    public float AsteroidWaveSpawnTime {
        get { return asteroidWaveSpawnTime; }
        private set { asteroidWaveSpawnTime = (value < 0) ? 0 : value; }
    }
    public int NumberOfAsteroidsInSingleWave {
        get { return numberOfAsteroidsInSingleWave; }
        private set { numberOfAsteroidsInSingleWave = ((int)value < 0) ? 0 : (int)value; }
    }
    public int NumberOfAsteroidsToSpawnAfterDestroy {
        get { return numberOfAsteroidsToSpawnAfterDestroy; }
        private set { numberOfAsteroidsToSpawnAfterDestroy = ((int)value < 0) ? 0 : (int)value; }
    }
    public int PointsForSmallRock {
        get { return pointsForSmallRock; }
        private set { pointsForSmallRock = ((int)value < 0) ? 0 : (int)value; }
    }
    public int PointsForMediumRock {
        get { return pointsForMediumRock; }
        private set { pointsForMediumRock = ((int)value < 0) ? 0 : (int)value; }
    }
    public int PointsForBigRock {
        get { return pointsForBigRock; }
        private set { pointsForBigRock = ((int)value < 0) ? 0 : (int)value; }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
    }

    public int GetHighscore() {
        return PlayerPrefs.GetInt("Highscore");
    }

    public void SetHighscore(int score) {
        if (score > GetHighscore()) {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}

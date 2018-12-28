using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D), typeof(Rigidbody2D))]
public class PlayerHealthController : MonoBehaviour {

    [SerializeField]
    private GameObject deathSoundEffectPrefab;
    [SerializeField]
    private GameObject respawnSoundEffectPrefab;
    [SerializeField]
    private GameObject explosionEffectPrefab;
    [SerializeField]
    private GameObject healthUIPrefab;
    [SerializeField]
    private Transform healthUIContainer;
    [SerializeField]
    private AsteroidWaveManager asteroidWaveManager;
    [SerializeField]
    private GameOverMenuController gameOverMenuController;
    [SerializeField]
    private BackgroundMusicPlayer backgroundMusicPlayer;
    [SerializeField]
    private MobileInputController mobileInputController;

    private int numberOfLives;
    private List<GameObject> healthUISpriteGO;
    
	private void Start () {
        healthUISpriteGO = new List<GameObject>();
        numberOfLives = GameManager.instance.NumberOfLives;
        for (int i = 0; i < numberOfLives; ++i) {
            GameObject go = Instantiate(healthUIPrefab);
            go.transform.SetParent(healthUIContainer);
            healthUISpriteGO.Add(go);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Equals("Asteroid")) {
            TakeDamage();
        }
    }

    public void TakeDamage() { 
        if (--numberOfLives == 0) {
            Destroy(healthUISpriteGO[numberOfLives]);
            Instantiate(deathSoundEffectPrefab);
            Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
            backgroundMusicPlayer.Stop();
            asteroidWaveManager.DestroyAllAsteroids(true);
            gameOverMenuController.ShowGameOverPanel();

            #if UNITY_ANDROID || UNITY_IOS
                mobileInputController.SetChildButtonsActive(false);
            #endif

        } else {
            Destroy(healthUISpriteGO[numberOfLives]);
            healthUISpriteGO.RemoveAt(numberOfLives);
            StartCoroutine("RespawnCoroutine");
        }
    }

    private IEnumerator RespawnCoroutine() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Collider2D c = GetComponent<Collider2D>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        PlayerMovement pm = GetComponent<PlayerMovement>();

        //Disable collider, 'hide' spaceship and reset its position + rotation + velocity + sound/anim states
        pm.ResetFXStates();
        c.enabled = false;
        pm.enabled = false;
        sr.enabled = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        transform.position = Vector2.zero;
        transform.rotation = Quaternion.identity;
        
        #if UNITY_ANDROID || UNITY_IOS
            mobileInputController.SetChildButtonsActive(false);
        #endif
        
        asteroidWaveManager.DestroyAllAsteroids(true);
        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Instantiate(deathSoundEffectPrefab);
        backgroundMusicPlayer.Stop();

        yield return new WaitForSeconds(GameManager.instance.RespawnTime);

        Instantiate(respawnSoundEffectPrefab);
        backgroundMusicPlayer.Play();

        sr.enabled = true;
        c.enabled = true;
        pm.enabled = true;

        #if UNITY_ANDROID || UNITY_IOS
            mobileInputController.SetChildButtonsActive(true);
        #endif
    }
}

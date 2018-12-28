using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BulletLogic : MonoBehaviour {

    private bool registerCollisionsWithPlayer;

	private void Start() {
        Destroy(gameObject, GameManager.instance.BulletDestroyTime);
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Equals("Player")) {
            //Bullets are being instantiated from 'inside' of our spaceship which means
            //that we have to ignore first collision with ship which will always occur.
            if (!registerCollisionsWithPlayer) {
                registerCollisionsWithPlayer = true;
                return;
            } else {
                PlayerHealthController phc = collision.GetComponent<PlayerHealthController>();
                if (phc != null) {
                    collision.GetComponent<PlayerHealthController>().TakeDamage();
                } else {
                    Debug.Log("Bullet logic | GetComponent<PlayerHealthController>() == null");
                }
            }
        } else if (collision.tag.Equals("Asteroid")) {
            AsteroidLogic al = collision.GetComponent<AsteroidLogic>();
            if (al != null) {
                al.DestroyWithPoints();
            } else {
                    Debug.Log("Bullet logic | GetComponent<AsteroidLogic>() == null");
            }
        }

        Destroy(gameObject);
    }
}

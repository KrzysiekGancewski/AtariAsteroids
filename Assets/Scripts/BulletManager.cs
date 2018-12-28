using UnityEngine;

public class BulletManager : MonoBehaviour {

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject shootSoundEffect;
    [SerializeField]
    private Transform bulletEjectionPoint;

    public void Shoot() {
        GameObject go = Instantiate(bulletPrefab, bulletEjectionPoint.position, bulletEjectionPoint.rotation);
        Rigidbody2D bulletRigidbody = go.GetComponent<Rigidbody2D>();
        if (bulletRigidbody == null) {
            Debug.Log("BulletManager | bullet's rigidbody == null");
            return;
        }
        
        bulletRigidbody.AddForce(transform.up * GameManager.instance.BulletSpeed, ForceMode2D.Impulse);
        Instantiate(shootSoundEffect);
    }
}

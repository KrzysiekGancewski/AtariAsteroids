using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody2D), typeof(BulletManager))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

    private AudioSource audioSource;
    private Animator animator;
    private new Rigidbody2D rigidbody;
    private BulletManager bulletManager;
    private float verticalInput;
    private float horizontalInput;
    private float spaceshipMaxSpeed;
    private float spaceshipTurnSpeed;

    private void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
        bulletManager = GetComponent<BulletManager>();
        spaceshipMaxSpeed = GameManager.instance.SpaceshipMaxSpeed;
        spaceshipTurnSpeed = GameManager.instance.SpaceshipTurnSpeed;
    }

    private void Update() {
        #if UNITY_STANDALONE
            ReadKeyboardInput();
        #endif

        ManageThrustSoundEffect();
        ManageThrustAnimation();
    }
    
    private void FixedUpdate() {
        //Due to lack of gravity and linear drag we can not rely on AddRelativeForce function, otherwise we would have no speed limit.
        //What is more, this approach allows player to accelerate(especially in the opposite direction) more dynamically.
        if (verticalInput != 0) {
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, transform.up * verticalInput * spaceshipMaxSpeed, 0.02f);
        }

        rigidbody.AddTorque(-horizontalInput * spaceshipTurnSpeed);
    }

    private void ManageThrustAnimation() {
        if (verticalInput != 0 && !animator.GetBool("isAccelerating")) {
            animator.SetBool("isAccelerating", true);
        } else if (verticalInput == 0 && animator.GetBool("isAccelerating")) {
            animator.SetBool("isAccelerating", false);
        }
    }

    private void ManageThrustSoundEffect() {
        if (verticalInput != 0 && !audioSource.isPlaying) {
            audioSource.Play();
        } else if (verticalInput == 0 && audioSource.isPlaying) {
            audioSource.Stop();
        }
    }

    private void ReadKeyboardInput() {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space)) {
            bulletManager.Shoot();
        }
    }

    public void ResetFXStates() {
        animator.SetBool("isAccelerating", false);
        audioSource.Stop();
    }

    #region Mobile input functions
    public void ForwardButton(bool isPressed) { 
        if (isPressed) {
            verticalInput = 1f;
        } else { 
            verticalInput = 0f;
        }
    }

    public void BackwardButton(bool isPressed) { 
        if (isPressed) {
            verticalInput = -1f;
        } else { 
            verticalInput = 0f;
        }
    }

    public void LeftButton(bool isPressed) { 
        if (isPressed) {
            horizontalInput = -1f;
        } else {
            horizontalInput = 0f;
        }
    }

    public void RightButton(bool isPressed) { 
        if (isPressed) {
            horizontalInput = 1f;
        } else {
            horizontalInput = 0f;
        }
    }

    public void ShootButton() {
        bulletManager.Shoot();
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce;
    public float gravityModifier;
    public int jumpAllow = 2;
    private int jumpRemaining = 2;
    public float accelerationModifier = 1.5f;
    public bool acceleration = false;
    public bool gameOver;

    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;

        jumpRemaining = jumpAllow;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpRemaining > 0 && !gameOver)
        {
            jumpRemaining--;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        if (!acceleration && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            acceleration = true;

            MoveLeft.speed *= accelerationModifier;
            float runningSpeed = playerAnim.GetFloat("Speed_f");
            playerAnim.SetFloat("Speed_f", runningSpeed * accelerationModifier);
        }
        else if (acceleration && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            acceleration = false;

            MoveLeft.speed /= accelerationModifier;
            float runningSpeed = playerAnim.GetFloat("Speed_f");
            playerAnim.SetFloat("Speed_f", runningSpeed / accelerationModifier);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameOver) return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            dirtParticle.Play();
            jumpRemaining = jumpAllow;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(crashSound, 1.0f);
            dirtParticle.Stop();
            explosionParticle.Play();
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            Debug.Log("Game over!");
        }
    }
}

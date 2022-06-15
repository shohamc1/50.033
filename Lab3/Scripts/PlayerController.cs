using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed, upSpeed;
    public float maxSpeed = 10;
    private Rigidbody2D marioBody;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    // public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;
    public Button resetButton;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    public ParticleSystem dustCloud;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Back on ground!");
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            scoreText.text = "Score: " + score.ToString();
            dustCloud.Play();
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true; //check if Gomba is underneath
            Debug.Log("countScoreState is true");
            PlayJumpSound();
        }
    }

    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        marioAnimator.SetBool("onGround", onGroundState);

        if (Input.GetKeyDown("a") && faceRightState)
        {
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
            faceRightState = true;
            marioSprite.flipX = false;
        }

        // when jumping, and Gomba is near Mario and we haven't registered our score
        if (!onGroundState && countScoreState)
        {
            // if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 2.5f)
            // {
            //     countScoreState = false;
            //     score++;
            //     Debug.Log(score);
            // }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba!");
            Time.timeScale = 0.0f;
            resetButton.gameObject.SetActive(true);
        }
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }
}

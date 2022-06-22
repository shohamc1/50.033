using System.Collections;
using UnityEngine;


public class PlayerControllerEV : MonoBehaviour
{
    private float force;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameConstants gameConstants;

    private bool isDead = false;
    private bool isADKeyUp = true;
    private bool isSpacebarUp = true;
    private bool onGroundState = true;
    private bool faceRightState = true;
    private bool countScoreState = false;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    public ParticleSystem dustCloud;
    public CastEvent consumePowerup;

    void Start()
    {
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();

        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerStartingMaxSpeed);
        force = gameConstants.playerDefaultForce;
    }

    void Update()
    {
        if (Input.GetKeyDown("d"))
        {
            isADKeyUp = false;
            faceRightState = true;
        }

        if (Input.GetKeyUp("d"))
        {
            isADKeyUp = true;
        }

        if (Input.GetKeyDown("a"))
        {
            isADKeyUp = false;
            faceRightState = false;
        }

        if (Input.GetKeyUp("a"))
        {
            isADKeyUp = true;
        }

        if (Input.GetKeyDown("space"))
        {
            isSpacebarUp = false;
        }

        if (Input.GetKeyUp("space"))
        {
            isSpacebarUp = true;
        }

        if (Input.GetKeyDown("z"))
        {
            AttemptConsumePowerup(KeyCode.Z);
        }

        if (Input.GetKeyDown("x"))
        {
            AttemptConsumePowerup(KeyCode.X);
        }

        marioSprite.flipX = !faceRightState;
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        marioAnimator.SetBool("onGround", onGroundState);

    }

    public void AttemptConsumePowerup(KeyCode k)
    {
        consumePowerup.Raise(k);
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            //check if a or d is pressed currently
            if (!isADKeyUp)
            {
                float direction = faceRightState ? 1.0f : -1.0f;
                Vector2 movement = new Vector2(force * direction, 0);
                if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                    marioBody.AddForce(movement);
            }

            if (!isSpacebarUp && onGroundState)
            {
                marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
                onGroundState = false;
                // part 2
                marioAnimator.SetBool("onGround", onGroundState);
                countScoreState = true; //check if goomba is underneath
            }
        }
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacle"))
        {
            dustCloud.Play();
            onGroundState = true; // back on ground
            // countScoreState = false; // reset score state
            // scoreText.text = "Score: " + score.ToString();
        };
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    public void PlayerDiesSequence()
    {
        isDead = true;
        // marioAnimator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        marioBody.AddForce(Vector3.up * 30, ForceMode2D.Impulse);
        marioBody.gravityScale = 30;
        StartCoroutine(dead());
    }

    IEnumerator dead()
    {
        yield return new WaitForSeconds(1.0f);
        marioBody.bodyType = RigidbodyType2D.Static;
    }

}
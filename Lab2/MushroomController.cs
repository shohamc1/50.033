using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private bool launched = false;
    private bool playerCollision = false;
    private bool right = true;
    private Rigidbody2D mushroomBody;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        mushroomBody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        launched = true;
    }

    private void FixedUpdate()
    {
        if (!launched || playerCollision)
            return;

        var velocity = mushroomBody.velocity;
        velocity = right ? new Vector2(speed, velocity.y) : new Vector2(-speed, velocity.y);
        mushroomBody.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Obstacles"))
        {
            right = !right;
        }

        if (!playerCollision && other.gameObject.CompareTag("Player"))
        {
            playerCollision = true;
            mushroomBody.velocity = Vector2.zero;
            return;
        }

        var dir = other.GetContact(0).normal;
        if (dir == Vector2.up) return;

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

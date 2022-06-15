using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaThanos : MonoBehaviour
{
    private bool right = true;
    private Rigidbody2D mushroomBody;

    private Material material;

    bool isDissolving = false;
    float fade = 1f;
    public float speed;


    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        material = GetComponent<SpriteRenderer>().material;
    }

    private void FixedUpdate()
    {
        var velocity = mushroomBody.velocity;
        velocity = right ? new Vector2(speed, velocity.y) : new Vector2(-speed, velocity.y);
        mushroomBody.velocity = velocity;
    }

    private void Update()
    {
        if (isDissolving)
        {
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {
                fade = 0f;
                isDissolving = false;
                Destroy(gameObject);
            }

            // Set the property
            material.SetFloat("_Fade", fade);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacles") && !isDissolving)
        {
            Debug.Log("Back on Goomba ded!");
            right = !right;
            isDissolving = true;
        };
    }

}

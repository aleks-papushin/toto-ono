﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// TODO Think about and implement player states (like invulnerability after hurting, death etc.)

public class Eskimo : MonoBehaviour {
	public float speed = 10f;
    public int lives = 3;
    public float jumpForce;
    public float posX, posY;
	public bool grounded = false;
	public bool facingRight = false;
	public float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround;

    public float invulnerabilityTime = 4f;

    private float invulnerabilityTimer = 0;
    private float blinkingTimer = 0;
    private bool invulnerable = false;

	Rigidbody2D rig;

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody2D>();	
        posX = rig.position.x;
        posY = rig.position.y;
        jumpForce = 1500f;
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

		float move = Input.GetAxis("Horizontal");
		rig.velocity = new Vector2(move * speed, rig.velocity.y);

		if ((Input.GetKeyDown(KeyCode.UpArrow) || 
			Input.GetKeyDown(KeyCode.W) ||
			Input.GetKeyDown(KeyCode.Space)) &&
			grounded) 
		{
            rig.AddForce (new Vector2 (0, jumpForce));
		}

        // Check for looking forward
		if (move > 0 && facingRight) 
        {
			Flip();			
		} 
        else if (move < 0 && !facingRight) 
        {
			Flip();
		}		
        
        // Check for invulnerability
        if (invulnerable)
        {
            Invulnerability();
        }	
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Hurt();
        }
    }

    // Lives management, invulnerability applying
    protected void Hurt()
    {
        lives--;
        if(lives > 0)
        {
            Physics2D.IgnoreLayerCollision(8, 9, true); // invulnarability against enemies turn on
            invulnerable = true;                       
        }
        else
        {
            gameObject.SetActive(false); // Temporarily death implemetnation
            SceneManager.LoadScene("Start");
        }        
    }

    protected void Invulnerability()
    {
        invulnerabilityTimer += Time.deltaTime;

        Blink();

        if (invulnerabilityTimer >= invulnerabilityTime)
        // Return to regular state
        {
            GetComponent<SpriteRenderer>().enabled = true;
            invulnerable = false;
            Physics2D.IgnoreLayerCollision(8, 9, false);
            invulnerabilityTimer = 0;
        }
    }

    protected void Blink()
    {
        blinkingTimer += Time.deltaTime;
        if (blinkingTimer > 0.15f)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            blinkingTimer = 0;
        }
    }

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
        transform.localScale = theScale;
	}

    // Just example. Will be replaced later
    void OnGUI()
    {
        if(lives > 0)
        {
            GUI.Box(new Rect(0, 0, 100, 50), "Lives = " + lives);
        }
        else
        {
            GUI.Box(new Rect(0, 0, 100, 50), "Game Over");
        }
        
    }
}
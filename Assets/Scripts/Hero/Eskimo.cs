﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// TODO Think about and implement player states (like invulnerability after hurting, death etc.)
// Think about all moving objects implement the same class or interface

public class Eskimo : MonoBehaviour {
	public float speed = 10f;
    public int lives = 3;
    public float jumpForce;
    public float posX, posY;
	public bool grounded = false;
    public bool jump = false;
	public bool facingRight = false;
	public float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround;

    public float invulnerabilityTime = 4f;

    private float invulnerabilityTimer = 0;
    private float blinkingTimer = 0;
    private bool invulnerable = false;

	private Rigidbody2D rig;
    private SpriteRenderer[] sprites;
    private Animator playerAnimator;

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody2D>();	
        playerAnimator = GetComponent<Animator>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        posX = rig.position.x;
        posY = rig.position.y;
        //jumpForce = 1500f;
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        HandleInput();
        HandleMovement(horizontal);
        
        // Check for invulnerability
        if (invulnerable)
        {
            Invulnerability();
        }

        ResetValues();
    }

    private void HandleInput()
    {
        // Handling jumping
        if ((Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.Space)) &&
            grounded)
        {            
            jump = true;
        }
    }

    private void HandleMovement(float move)
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        
        rig.velocity = new Vector2(move * speed, rig.velocity.y);

        playerAnimator.SetFloat("speed", Mathf.Abs(move));

        if (grounded)
        {
            playerAnimator.SetBool("landing", false);

            if (jump)
            {
                rig.AddForce(new Vector2(0, jumpForce));
                playerAnimator.SetBool("jump", true);
            }
        }

        // If player falling down
        if (rig.velocity.y < -0.99f)
        {
            playerAnimator.SetBool("jump", false);
            playerAnimator.SetBool("landing", true);
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
        if (blinkingTimer > 0.15f) // maybe replace this hardcode later
        {
            foreach (SpriteRenderer renderer in sprites)
            {
                renderer.enabled = !renderer.enabled;
            }
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

    // Resets all the values should be reseted after applying in single Update method run
    private void ResetValues()
    {
        jump = false;
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
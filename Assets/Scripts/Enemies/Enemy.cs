﻿using UnityEngine;
using System.Collections;

// TODO Implement jumping enemy derrived class

public class Enemy : MovingObject {

    public Sprite usualSprite;
    public Sprite unconsiousSprite;
    SpriteRenderer sr;

    public bool justHurted = false;
    protected float hurtCooldown = 0.2f; // in seconds

    // If 1 life remains object frezees, uncosious turns to true
    protected bool unconsious = false;

    protected override void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = usualSprite;
    }

    protected void Update () 
    {
        if (!unconsious)
        {
            Move();            
        }

        // Protects from hurting from nearest FloorSquare at the same time if justHurted
        // while hurtCooldown will not expired
        if (justHurted)
        {
            JustHurtCheck();
        }
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        // Change direction if collided with any object excepting Floor
        if (col.gameObject.tag != "Floor" &&
            col.gameObject.tag != "Teleport" && 
            !col.collider.isTrigger)
        {
            moveRight = !moveRight;
            //Flip();
        }

        // If taken collision from nearest flor that was not previously collided with
        if (!justHurted && col.gameObject.tag == "HurtingFloor")
        {
            Debug.Log("Collision with hurtingFloor!");
            Hurt();
        }

        if (lives == 1 && col.gameObject.tag == "Player")
        {
            gameObject.SetActive(false); // Temporarily death implemetnation
        }
    }

    protected void OnCollisionExit2D(Collision2D col)
    {
        // Enemy just collided with this object so testing in this method
        //if (!justHurted && col.gameObject.tag == "HurtingFloor")
        if (!justHurted && col.gameObject.layer == 10)
            {
            Debug.Log("Collision with hurtingFloor!");
            Hurt();
        }
    }

    // Hurts if not justHurted and not unconsious
    protected void Hurt()
    {
        if (!unconsious)
        {
            lives--;
            justHurted = true;

            if (lives == 1)
            {
                unconsious = true;
                sr.sprite = unconsiousSprite;
                tag = "UnconsciousEnemy";
            }
        }
        else
        {
            lives++;
            unconsious = false;
            sr.sprite = usualSprite;
            tag = "Enemy";
        }
    }

    protected void Flip()
    {
        Debug.Log("In Flip()");
        facingRight = !facingRight;
        foreach (SpriteRenderer spriteRend in sprites)
        {
            Vector3 theScale = spriteRend.transform.localScale;
            theScale.x *= -1;
            spriteRend.transform.localScale = theScale;
        }
    }

    protected void JustHurtCheck()
    {
        timer += Time.deltaTime;
        if (timer > hurtCooldown)
        {
            timer = 0;
            justHurted = false;
        }
    }
}
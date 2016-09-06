using UnityEngine;
using System.Collections;

// TODO Implement jumping enemy derrived class

public class Enemy : MovingObject {
    public int testingHurtAmount = 0;

    protected bool justHurted = false;
    protected float hurtCooldown = 0.5f; // in seconds

    // If 1 life remains object frezees, uncosious turns to true
    protected bool unconsious = false;

    public Enemy()
    {
    }

	// Update is called once per frame
    protected void Update () 
    {
        Move();

        // Protects from hurting from nearest FloorSquare at the same time if justHurted
        // while hurtCooldown will not expired
        if (justHurted)
        {
            timer += Time.deltaTime;
            if (timer > hurtCooldown)
            {
                timer = 0;
                justHurted = false;
            }
        }
	}

    protected override void OnCollisionEnter2D(Collision2D col)
    {        
        // Change direction if collided with any object excepting Floor
        if (col.gameObject.tag != "Floor" &&
            col.gameObject.tag != "Teleport" && 
            !col.collider.isTrigger)
        {
            moveRight = !moveRight;
        }
    }

    protected void OnCollisionExit2D(Collision2D col)
    {        
        Hurt(col);
    }

    // Hurts if not justHurted 
    protected void Hurt(Collision2D col)
    {        
        if (!justHurted && col.gameObject.tag == "HurtingFloor")
        {
            testingHurtAmount++;
            Debug.Log("OnCollisionExit with HurtingFloor registered\n" + "HurtAmount = " + testingHurtAmount);
            if (!unconsious)
            {
                lives--;
                justHurted = true;

                if (lives == 1)
                {
                    unconsious = true;
                }
            }
            else
            {
                lives++;
                unconsious = false;                
            }
        }              
    }
}
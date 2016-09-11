using UnityEngine;
using System.Collections;

// TODO In purpose of trying to improve collision with enemies modify colliders form
// TODO Think and implement FloorTiangle and general class for floors

public class FloorSquare : MonoBehaviour {
    protected float timer = 0;
    protected float hurtingTime = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //if (tag == "HurtingFloor")
        if (gameObject.layer == 10)
        {
            // Check if enough time for hurting
            CheckHurtTime();
        }
                   	
	}

    // Allow to change tag if hurtingTime expired
    protected void CheckHurtTime()
    {
        timer += Time.deltaTime;
        if (timer > hurtingTime)
        {
            //tag = "Floor";
            gameObject.layer = 0;
            timer = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // If collision with player and he placed below - turn Floor to HurtingFloor for enemy hurting
        if (col.gameObject.tag == "Player" && col.transform.position.y < transform.position.y)
        {
            //Debug.Log("HurtingFloor activated!");
            //tag = "HurtingFloor";
            gameObject.layer = 10;
        }
    }
}

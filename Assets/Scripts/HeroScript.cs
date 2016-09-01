using UnityEngine;
using System.Collections;

public class HeroScript : MonoBehaviour {
	public float speed = 8f;
    public int score = 0;
    public float posX, posY;
	public bool grounded = false;
	public bool facingRight = false;
	public float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround;

	Rigidbody2D rig;

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody2D>();	
        posX = rig.position.x;
        posY = rig.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

		float move;
		move = Input.GetAxis("Horizontal");
		rig.velocity = new Vector2(move * speed, rig.velocity.y);

		if ((Input.GetKeyDown(KeyCode.UpArrow) || 
			Input.GetKeyDown(KeyCode.W) ||
			Input.GetKeyDown(KeyCode.Space)) &&
			grounded) 
		{
			rig.AddForce (new Vector2 (0, 700f));
		}

		if (move > 0 && facingRight) 
        {
			Flip();			
		} 
        else if (move < 0 && !facingRight) 
        {
			Flip();
		}
			
    }

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PolygonCollider2D>().tag == "Star")
        {
            Destroy(col.gameObject);
            score++;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            rig.position = new Vector2(posX, posY);
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 100), "Score = " + score);
    }
}
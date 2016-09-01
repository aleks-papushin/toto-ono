using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour {
    public float speed = 1f;
    public bool moveRight = true;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float posX, posY;

    protected Rigidbody2D rig;

    private BoxCollider2D boxCollider;

	// Use this for initialization
	protected virtual void Start () 
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();
        posX = rig.position.x;
        posY = rig.position.y;
	}

    protected virtual void Move()
    {
        if (moveRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            speed *= -1;
        }
    }
}
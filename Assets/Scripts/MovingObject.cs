using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour {
    public float speed = 1f;
    public int lives = 1;
    public bool moveRight = true;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float posX, posY;

    public float timer;
    protected Rigidbody2D rig;

	// Use this for initialization
	protected virtual void Start () 
    {
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
        if (col.gameObject.tag != "Floor" && !col.collider.isTrigger)
        {
            speed *= -1;
        }
    }
}
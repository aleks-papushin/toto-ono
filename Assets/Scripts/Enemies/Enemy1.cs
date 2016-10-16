using UnityEngine;
using System.Collections;

// TODO Think about and implement general class
// TODO Implement jumping enemy class

public class Enemy1 : MonoBehaviour {
    public float speed;
    public int lives = 2;
    public bool moveRight = true;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float posX, posY;

    public float timer;
    protected Rigidbody2D rig;
    protected SpriteRenderer[] sprites;
    private Animator enemyAnimator;

    public bool facingRight = true;
    public bool justHurted = false;
    protected float hurtCooldown = 0.2f; // in seconds

    // If 1 life remains object frezees, uncosious turns to true
    protected bool unconsious = false;

    protected void Awake()
    {
        speed = 2f;
    }

    protected void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        posX = rig.position.x;
        posY = rig.position.y;

        sprites = GetComponentsInChildren<SpriteRenderer>();

        enemyAnimator = GetComponent<Animator>();

        // Flip sprites if obect goes left at start
        if (!moveRight)
        {
            facingRight = false;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }        
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

    protected virtual void Move()
    {
        if (moveRight)
        {
            rig.velocity = new Vector2(speed, rig.velocity.y);
        }
        else
        {
            rig.velocity = new Vector2(-speed, rig.velocity.y);
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
            Flip();
        }

        // If taken collision from nearest flor that was not previously collided with
        if (!justHurted && col.gameObject.tag == "HurtingFloor")
        {
            //Debug.Log("Collision with hurtingFloor!");
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
            //Debug.Log("Collision with hurtingFloor!");
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
                //sr.sprite = unconsiousSprite;
                tag = "UnconsciousEnemy";
                enemyAnimator.SetBool("unconscious", true);
            }
        }
        else
        {
            lives++;
            unconsious = false;
            //sr.sprite = usualSprite;
            tag = "Enemy";
            enemyAnimator.SetBool("unconscious", false);
        }
    }

    protected void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
using UnityEngine;
using System.Collections;

// TODO Think about improve teleportation - so objects entring to In and Out portals at the same time teleport successfully
// TODO Add variable to know should we switch enemy direction

public class Teleportation : MonoBehaviour {
    public Transform thisPortal;
    public Transform outPortal;
    public bool teleportPlayer = true;

    // Use this for initialization
    void Start () 
    {
        
    }
	
	// Update is called once per frame
	void Update () 
    {
        
    }

    protected void OnCollisionEnter2D(Collision2D other) 
    {
        // Check if we can teleport this object
        if (CheckIfShouldTeleport(other))
        {
            // Teleport other collider
            Teleport(other);
        }
        else
        {
            // Allow object that we do not want to teleport pass through collider
            GetComponent<EdgeCollider2D>().isTrigger = true;
        }
    }

    protected bool CheckIfShouldTeleport(Collision2D other)
    {
        return (other.gameObject.tag == "Player" && teleportPlayer) || other.gameObject.tag != "Player";
    }

    protected void Teleport(Collision2D other)
    {

        // Switch destination collider as trigger in purpose to not teleport back 
        // (OnCollisionEnter2D of destination doesn't work for collider if it is trigger)
        outPortal.GetComponent<EdgeCollider2D>().isTrigger = true;

        other.transform.position = new Vector3(
            outPortal.position.x,
            outPortal.position.y,
            other.transform.position.z);
    }

    // When this Teleportation object is outPortal its collider setted to trigger by InPortal 
    // while it intersects teleported object collider then this function executes
    protected void OnTriggerExit2D(Collider2D other)
    {        
        if (GetComponent<EdgeCollider2D>().isTrigger)
        {
            GetComponent<EdgeCollider2D>().isTrigger = false;
        }
    }
}
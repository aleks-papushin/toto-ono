using UnityEngine;
using System.Collections;

// TODO Think about improve teleportation - so objects entring to In and Out portals at the same time teleport successfully

public class Teleportation : MonoBehaviour {
    public Transform thisPortal;
    public Transform outPortal;

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
        //differYPos = other.transform.position.y - outPortal.transform.position.y;
        // Teleport other collider
        Teleport(other);
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
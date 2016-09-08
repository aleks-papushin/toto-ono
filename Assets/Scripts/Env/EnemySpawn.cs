using UnityEngine;
using System.Collections;

// TODO Implement logic when there is general spawn timer and spawners choosing consequentially
// TODO Implement anti-spawner where enemy jump-in and teleports

public class EnemySpawn : Spawner { 
	// Use this for initialization
	void Start () {
        maxObjects = 2;
        spawnInterval = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Spawn();
	}

    void Spawn()
    {
        // Simple counting for testing purposes
        if (objectsCounter < maxObjects)
        {
            if (timer > spawnInterval)
            {
                Rigidbody2D obj = Instantiate(objPrefab, gameObject.transform.position, gameObject.transform.rotation) as Rigidbody2D;
                obj.GetComponent<MovingObject>().moveRight = objMoveRight; 
                objectsCounter++; 

                timer = 0;
            }
            timer += Time.deltaTime;    
        }        
    }
}

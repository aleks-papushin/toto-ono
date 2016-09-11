using UnityEngine;
using System.Collections;

public class EnemySpawn : Spawner {

	// Use this for initialization
	void Start () {
        spawnDelay *= -1; 
        timer = spawnDelay; // init delay set       
	}
	
	// Update is called once per frame
	void Update () {
        Spawn();
	}

    void Spawn()
    {
        // Delay implementation
        if (timer < 0)
        {
            timer += Time.deltaTime;
        }
        // If there is no objects - instantiate immediately after delay expired
        else if (objectsCounter == 0)
        {
            InstantiateObject();
            objectsCounter++;
            timer += Time.deltaTime;
        }
        else if (objectsCounter < maxObjects)
        {
            if (timer > spawnInterval)
            {
                InstantiateObject();
                timer = 0;
            }
            timer += Time.deltaTime;    
        }       
    }

    void InstantiateObject()
    {
        Rigidbody2D obj = Instantiate(objPrefab, gameObject.transform.position, gameObject.transform.rotation) as Rigidbody2D;
        obj.GetComponent<MovingObject>().moveRight = objMoveRight;
        objectsCounter++;
    }
}

using UnityEngine;
using System.Collections;

public abstract class Spawner : MonoBehaviour {
    public Rigidbody2D objPrefab;

    // Allow to define spawned object default direction
    public bool objMoveRight = true;

    public int maxObjects;
    public float spawnInterval;
    public float spawnDelay; 

    protected float timer = 0f;    
    protected int objectsCounter = 0; // for simple tracking 
    protected ArrayList objects = new ArrayList(); // for advanced tracking
}
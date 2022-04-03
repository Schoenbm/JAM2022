using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float lifeSpan;
    float timeToDespawn;

    GameManager gameManager;

    public Platform platform;
    void Start()
    {
        timeToDespawn = lifeSpan;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timeToDespawn <= 0){
            timeToDespawn = lifeSpan;
            platform.hasItem = false;
            Destroy(this.gameObject);
        }
        timeToDespawn -= Time.deltaTime;
    }
}

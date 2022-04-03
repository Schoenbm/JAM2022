using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float lifeSpan;
    float timeToDespawn;

    public GameManager gameManager;
    void Start()
    {
        timeToDespawn = lifeSpan;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timeToDespawn <= 0){
            timeToDespawn = lifeSpan;
            Destroy(this.gameObject);
        }
        timeToDespawn -= Time.deltaTime;
    }
}

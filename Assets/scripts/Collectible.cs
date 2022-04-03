using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float lifeSpan;
    float timeToDespawn;
    SpriteRenderer spr;
    GameManager gameManager;

    public float SpinSpeed = 0;
    public float WaveAmplitude = 0;
    public Platform platform;
    void Start()
    {
        spr = this.gameObject.GetComponent<SpriteRenderer>();
        timeToDespawn = lifeSpan;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Wave();
        Spin();
        if(timeToDespawn <= 0){
            timeToDespawn = lifeSpan;
            platform.hasItem = false;
            Destroy(this.gameObject);
        }
        timeToDespawn -= Time.deltaTime;
        spr.color = new Color(1, 1, 1, Mathf.Min(3 *timeToDespawn / lifeSpan));
    }

    public void Spin()
    {
        Vector3 rotation = new Vector3(0, 0,  SpinSpeed * Time.deltaTime);
        this.transform.eulerAngles += rotation;
    }

    public void Wave()
    {
        Vector3 translation = new Vector3(0, 0, WaveAmplitude * Time.time);
        this.transform.position += translation;
    }
}

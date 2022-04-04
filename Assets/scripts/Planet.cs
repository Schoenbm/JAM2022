using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public GameManager gameManager;
    public float maxHealth = 5000;
    public float health;
    float timeBetweenHpLoss = 0.5f;
    float timeBeforeNextHpLoss = 0;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(health <= 0){
            FindObjectOfType<AudioManager>().Play("Planet Explode");
            gameManager.EndGame();
        }
        if(timeBeforeNextHpLoss <= 0f){
            health-=10;
            timeBeforeNextHpLoss = timeBetweenHpLoss;
            //Debug.Log(health);
        }        
        timeBeforeNextHpLoss -= Time.deltaTime;
    }

    public void loseHealth(int amount){
        health -= amount;
        if(health <= 0){
            //EndGame();
        }            
    }

    public void plusHealth(int amount) {
        health += amount;
        Debug.Log("Planet healled");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public GameManager gameManager;
    int health = 5000;
    float timeBetweenHpLoss = 2f;
    float timeBeforeNextHpLoss = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdateUpdate()
    {
        if(health <= 0){
            gameManager.EndGame();
        }
        if(timeBeforeNextHpLoss <= 0f){
            health-=5;
            timeBeforeNextHpLoss = timeBetweenHpLoss;
        }
        timeBeforeNextHpLoss += Time.deltaTime;
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

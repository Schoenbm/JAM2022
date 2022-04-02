using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    int health = 5000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loseHealth(int amount){
        health -= amount;
        if(health <= 0){
            //EndGame();
        }            
    }
}

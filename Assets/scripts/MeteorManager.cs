using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject MeteorPrefab;
    public GameObject parent;

    public GameObject earthCore;
    
    public float timeBetweenMeteors;
    public float maxTimeBetweenMeteors;
    public float minTimeBetweenMeteors;
    float timeBeforeNextMeteor = 0f;
    int amount; //Need to scale it to difficulty or time elapsed.
    public int maxAmount;
    public int minAmount;
    public Planet myPlanet;

    void Start(){
        maxTimeBetweenMeteors = 2.5f;
        minTimeBetweenMeteors = 0.3f;
        timeBetweenMeteors = maxTimeBetweenMeteors;
        minAmount = 4;
        amount = minAmount;
        maxAmount = 8;
        //myPlanet = earthCore.transform.GetChild(0).GetComponent<Planet>();
    }

    // Update is called once per frame
    void Update()
    {
        amount = Mathf.Max(minAmount, maxAmount - Mathf.FloorToInt(maxAmount*(myPlanet.health/myPlanet.maxHealth)));
        if(amount > maxAmount){
            amount = maxAmount;
        }
        
        timeBetweenMeteors = Mathf.Max(minTimeBetweenMeteors,(maxTimeBetweenMeteors*(myPlanet.health/myPlanet.maxHealth)));

        if(!gameManager.gameOver && timeBeforeNextMeteor <= 0f){
            for(int i = 0; i < amount; i++){
                timeBeforeNextMeteor = timeBetweenMeteors;
                int angle = (int)Random.Range(0f,359f);
                float posX = Mathf.Cos(angle);
                float posY = Mathf.Sin(angle);
                Vector3 position = 90 * new Vector3(posX,posY,0);
                GameObject meteor = Instantiate(MeteorPrefab, position, Quaternion.identity) as GameObject;
                meteor.transform.parent = parent.transform;
                meteor.GetComponent<Meteor>().gameManager = gameManager;
            }
            
        }
        timeBeforeNextMeteor -= Time.deltaTime;
    }
}

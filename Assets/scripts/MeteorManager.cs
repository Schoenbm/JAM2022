using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject MeteorPrefab;
    public GameObject parent;
    
    public float timeBetweenMeteors = 5f;
    float timeBeforeNextMeteor = 0f;
    int amount = 5; //Need to scale it to difficulty or time elapsed.

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.gameOver && timeBeforeNextMeteor <= 0f){
            for(int i = 0; i < amount; i++){
                timeBeforeNextMeteor = timeBetweenMeteors;
                int angle = (int)Random.Range(0f,359f);
                float posX = Mathf.Cos(angle);
                float posY = Mathf.Sin(angle);
                Vector3 position = 70 * new Vector3(posX,posY,0);
                GameObject meteor = Instantiate(MeteorPrefab, position, Quaternion.identity) as GameObject;
                meteor.transform.parent = parent.transform;                
            }
            
        }
        timeBeforeNextMeteor -= Time.deltaTime;
    }
}

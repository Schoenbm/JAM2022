using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject icePrefab;
    public GameObject scrapPrefab;
    public GameObject earthCore;

    public float timeBetweenItemsSpawn = 3f;
    float timeBeforeNextItemSpawn;

    public float lifeSpanIce;
    public float lifeSpanScrap;
    public int amount = 3;
    int amountIce = 0;
    int amountScrap = 0;

    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        lifeSpanIce = 10f;
        lifeSpanScrap = 30f;
        gameOver = false;
        timeBeforeNextItemSpawn = timeBetweenItemsSpawn;
        for(int i =0; i< 20; i++){
            InstanciateLayer1Platform(32.5f, 35) ;
        }
        for(int i =0; i< 10; i++){
            InstanciateLayer1Platform(36.5f, 38);
        }
        for (int i = 0; i < 10; i++)
        {
            InstanciateLayer1Platform(38.5f, 42);
        }
        for (int i = 0; i < 20; i++)
        {
            InstanciateLayer1Platform(45, 50);
        }

        for (int i = 0; i < 20; i++)
        {
            InstanciateLayer1Platform(54, 60);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(!gameOver && timeBeforeNextItemSpawn <= 0f){
            for(int i = 0; i < amount; i++){
                timeBeforeNextItemSpawn = timeBetweenItemsSpawn;
                    //InstanciateIceItemRandom();
                    InstanciateIceItemPlatform(); 
                    //InstanciateScrapItemRandom();
                    InstanciateScrapItemPlatform();
            }
            
        }
        timeBeforeNextItemSpawn -= Time.deltaTime;
    }

    void InstanciateLayer1Platform(float min, float max){
        int angle = (int)Random.Range(0f,359f);
        float posX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float posY = Mathf.Sin(angle * Mathf.Deg2Rad);
        float height = Random.Range(min,max);
        Vector3 position = height * new Vector3(posX,posY,0);
        GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity) as GameObject;
        platform.transform.eulerAngles = new Vector3(0,0,angle - 90);
        platform.transform.parent = earthCore.transform;

        if(!platform.GetComponent<Platform>().isCorrect){
            Destroy(platform.gameObject);
            Debug.Log("Error platform L1");
            InstanciateLayer1Platform(min, max);
        }
    }

    void InstanciateIceItemRandom(){
        int angle = (int)Random.Range(0f,359f);
        float posX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float posY = Mathf.Sin(angle * Mathf.Deg2Rad);
        float height = Random.Range(32f,40f);
        Vector3 position = height * new Vector3(posX,posY,0);
        GameObject ice = Instantiate(icePrefab, position, Quaternion.identity);
        ice.transform.parent = earthCore.transform;
        ice.transform.eulerAngles = new Vector3(0,0,angle-90f);
        ice.GetComponent<Collectible>().lifeSpan = lifeSpanIce;
        amountIce++;
    }

    void InstanciateScrapItemRandom(){
        int angle = (int)Random.Range(0f,359f);
        float posX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float posY = Mathf.Sin(angle * Mathf.Deg2Rad);
        float height = Random.Range(32f,40f);
        Vector3 position = height * new Vector3(posX,posY,0);
        GameObject scrap = Instantiate(scrapPrefab, position, Quaternion.identity);
        scrap.transform.parent = earthCore.transform;
        scrap.transform.eulerAngles = new Vector3(0,0,angle-90f);
        scrap.GetComponent<Collectible>().lifeSpan = lifeSpanScrap;
        amountScrap++;
    }

    void InstanciateIceItemPlatform(){
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        float angle = Mathf.Atan2(earthCore.transform.position.normalized.y , earthCore.transform.position.normalized.x);

        int index = (int)Random.Range(0,platforms.Length-1);
        while(platforms[index].GetComponent<Platform>().hasItem){
            index = (int)Random.Range(0,platforms.Length-1);
        }
        platforms[index].GetComponent<Platform>().hasItem = true;
        Vector3 position = platforms[index].transform.position;
        Vector3 rotation = platforms[index].transform.rotation.eulerAngles;

        GameObject ice = Instantiate(icePrefab, position, Quaternion.identity, earthCore.transform);
        ice.transform.parent = earthCore.transform;
        ice.transform.eulerAngles = rotation;    
        ice.transform.position += 2*position.normalized;
        ice.GetComponent<Collectible>().lifeSpan = lifeSpanIce;
        amountIce++;
    }

    void InstanciateScrapItemPlatform(){
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        float angle = Mathf.Atan2(earthCore.transform.position.normalized.y , earthCore.transform.position.normalized.x);

        int index = (int)Random.Range(0,platforms.Length-1);
        while(platforms[index].GetComponent<Platform>().hasItem){
            index = (int)Random.Range(0,platforms.Length-1);
        }
        platforms[index].GetComponent<Platform>().hasItem = true;
        Vector3 position = platforms[index].transform.position;
        Vector3 rotation = platforms[index].transform.rotation.eulerAngles;

        GameObject scrap = Instantiate(scrapPrefab, position, Quaternion.identity, earthCore.transform);
        scrap.transform.parent = earthCore.transform;
        scrap.transform.eulerAngles = rotation;
        scrap.transform.position += 2*position.normalized;
        scrap.GetComponent<Collectible>().lifeSpan = lifeSpanScrap;
        amountScrap++;
    }

    public void EndGame(){
        gameOver = true;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach(GameObject go in platforms){
            Destroy(go);
        }
        GameObject[] iceItems = GameObject.FindGameObjectsWithTag("Ice");
        foreach(GameObject go in iceItems){
            Destroy(go);
        }
        GameObject[] scrapItems = GameObject.FindGameObjectsWithTag("Scrap");
        foreach(GameObject go in scrapItems){
            Destroy(go);
        }
    }

}
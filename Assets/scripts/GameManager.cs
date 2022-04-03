using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject icePrefab;
    public GameObject scrapPrefab;
    public GameObject earthCore;

    public float timeBetweenItemsSpawn = 3f;
    float timeBeforeNextItemSpawn = 0f;
    public int amount = 3;

    int maxEachItems = 20;
    int amountIce = 0;
    int amountScrap = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i< 20; i++){
            InstanciateLayer1Platform();
        }
        for(int i =0; i< 10; i++){
            InstanciateLayer2Platform();
        }
        for(int i =0; i< 10; i++){
            InstanciateLayer3Platform();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(timeBeforeNextItemSpawn <= 0f){
            for(int i = 0; i < amount; i++){
                timeBeforeNextItemSpawn = timeBetweenItemsSpawn;
                if(amountIce < maxEachItems){
                    //InstanciateIceItemRandom();
                    InstanciateIceItemPlatform(); 
                }
                    
                if(amountIce < maxEachItems){
                    //InstanciateScrapItemRandom();
                    InstanciateScrapItemPlatform();
                }
                     
                
                      
            }
            
        }
        timeBeforeNextItemSpawn -= Time.deltaTime;
    }

    void InstanciateLayer1Platform(){
        int angle = (int)Random.Range(0f,359f);
        float posX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float posY = Mathf.Sin(angle * Mathf.Deg2Rad);
        float height = Random.Range(32.5f,34);
        Vector3 position = height * new Vector3(posX,posY,0);
        GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity) as GameObject;
        platform.transform.eulerAngles = new Vector3(0,0,angle - 90);
        platform.transform.parent = earthCore.transform;

        if(!platform.GetComponent<Platform>().isCorrect){
            Destroy(platform.gameObject);
            Debug.Log("Error platform L1");
            //InstanciateLayer1Platform();
        }
    }

    void InstanciateLayer2Platform(){
        int angle = (int)Random.Range(0f,359f);
        float posX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float posY = Mathf.Sin(angle * Mathf.Deg2Rad);
        float height = Random.Range(36.5f,38);
        Vector3 position = height * new Vector3(posX,posY,0);
        GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity) as GameObject;
        platform.transform.eulerAngles = new Vector3(0,0,angle - 90);
        platform.transform.parent = earthCore.transform;

        if(!platform.GetComponent<Platform>().isCorrect){
            Destroy(platform.gameObject);
            Debug.Log("Error platform L2");
            InstanciateLayer2Platform();
        }
    }

    void InstanciateLayer3Platform(){
        int angle = (int)Random.Range(0f,359f);
        float posX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float posY = Mathf.Sin(angle * Mathf.Deg2Rad);
        float height = Random.Range(38.5f,42);
        Vector3 position = height * new Vector3(posX,posY,0);
        GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity) as GameObject;
        platform.transform.eulerAngles = new Vector3(0,0,angle - 90);
        platform.transform.parent = earthCore.transform;

        if(!platform.GetComponent<Platform>().isCorrect){
            Destroy(platform.gameObject);
            Debug.Log("Error platform L3");
            InstanciateLayer3Platform();
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
        amountScrap++;
    }

    void InstanciateIceItemPlatform(){
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        Debug.Log(platforms.Length);
        float angle = Mathf.Atan2(earthCore.transform.position.normalized.y , earthCore.transform.position.normalized.x);

        int index = (int)Random.Range(0,platforms.Length-1);
        Vector3 position = platforms[index].transform.position;
        Vector3 rotation = platforms[index].transform.rotation.eulerAngles;

        GameObject ice = Instantiate(icePrefab, position, Quaternion.identity, earthCore.transform);
        ice.transform.parent = earthCore.transform;
        ice.transform.eulerAngles = rotation;    
        ice.transform.position += position.normalized;
        amountIce++;
    }

    void InstanciateScrapItemPlatform(){
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        Debug.Log(platforms.Length);
        float angle = Mathf.Atan2(earthCore.transform.position.normalized.y , earthCore.transform.position.normalized.x);

        int index = (int)Random.Range(0,platforms.Length-1);
        Vector3 position = platforms[index].transform.position;
        Vector3 rotation = platforms[index].transform.rotation.eulerAngles;

        GameObject scrap = Instantiate(scrapPrefab, position, Quaternion.identity, earthCore.transform);
        scrap.transform.parent = earthCore.transform;
        scrap.transform.eulerAngles = rotation;
        scrap.transform.position += position.normalized;
        amountScrap++;
    }



}
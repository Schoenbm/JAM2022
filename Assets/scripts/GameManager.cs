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
        if(timeBeforeNextItemSpawn <= 0f){
            for(int i = 0; i < amount; i++){
                timeBeforeNextItemSpawn = timeBetweenItemsSpawn;
                if(amountIce < maxEachItems)
                    InstanciateIceItemRandom();
                if(amountIce < maxEachItems)
                    InstanciateScrapItemRandom();  
                
                //InstanciateIceItemPlatform();       
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
        float angle = Mathf.Atan2(this.transform.position.normalized.y , this.transform.position.normalized.x);

        int index = (int)Random.Range(0,platforms.Length-1);
        Vector3 position = platforms[index].transform.GetChild(0).gameObject.transform.position;
        //Quaternion rotation = platforms[index].transform.rotation;

        GameObject ice = Instantiate(icePrefab, position, Quaternion.identity, earthCore.transform);
        ice.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
        ice.transform.parent = earthCore.transform;
        ice.transform.position += new Vector3(0,1,0);
        amountIce++;
    }

    void InstanciateScrapItemPlatform(){
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        Debug.Log(platforms.Length);
        float angle = Mathf.Atan2(this.transform.position.normalized.y , this.transform.position.normalized.x);

        int index = (int)Random.Range(0,platforms.Length-1);
        Vector3 position = platforms[index].transform.GetChild(0).gameObject.transform.position;
        //Quaternion rotation = platforms[index].transform.rotation;

        GameObject scrap = Instantiate(scrapPrefab, position, Quaternion.identity, earthCore.transform);
        scrap.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
        scrap.transform.parent = earthCore.transform;
        scrap.transform.position += new Vector3(0,1,0);
        amountScrap++;
    }



}
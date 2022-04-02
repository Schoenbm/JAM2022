using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject parent;
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

    void InstanciateLayer1Platform(){
        int angle = (int)Random.Range(0f,359f);
        float posX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float posY = Mathf.Sin(angle * Mathf.Deg2Rad);
        float height = Random.Range(32.5f,34);
        Vector3 position = height * new Vector3(posX,posY,0);
        GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity) as GameObject;
        platform.transform.eulerAngles = new Vector3(0,0,angle - 90);
        platform.transform.parent = parent.transform;
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
        platform.transform.parent = parent.transform;
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
        platform.transform.parent = parent.transform;
        if(!platform.GetComponent<Platform>().isCorrect){
            Destroy(platform.gameObject);
            Debug.Log("Error platform L3");
            InstanciateLayer3Platform();
        }
    }


}
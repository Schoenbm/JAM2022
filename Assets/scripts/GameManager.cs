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
        for(int i =0; i< 10; i++){
            int angle = (int)Random.Range(0f,359f);
            float posX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float posY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 position = 33 * new Vector3(posX,posY,0);
            GameObject meteor = Instantiate(platformPrefab, position, Quaternion.identity) as GameObject;
            meteor.transform.eulerAngles = new Vector3(0,0,angle - 90);
            meteor.transform.parent = parent.transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
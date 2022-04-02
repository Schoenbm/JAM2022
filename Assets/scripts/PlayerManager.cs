using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    int lifes = 3;
    public GameObject playerPrefab;
    public GameObject planet;
    Vector3 position;

    GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(playerPrefab, new Vector3(0,31,0), Quaternion.identity) as GameObject;
        player.GetComponent<Movement>().earth = planet;
        player.GetComponent<Movement>().Manager = this;
    }

    // Update is called once per frame
    void Update(){      
    }

    public void playerDeath(){
        lifes -= 1;
        if(lifes <= 0){
            EndGame();
        }
        else{
            StartCoroutine(RespawnPlayer());
        }
    }

    IEnumerator RespawnPlayer(){        
        position = ClosestAvailablePosition(transform.position);
        Destroy(player);        
        yield return new WaitForSeconds(1.5f);
        player = Instantiate(playerPrefab, position, Quaternion.identity) as GameObject;
        player.GetComponent<Movement>().earth = planet;
        player.GetComponent<Movement>().Manager = this;
        yield return null;
    }

    Vector3 ClosestAvailablePosition(Vector3 position){
        Vector3 finalPos;
        finalPos = position + new Vector3(-1,5,0);
        return finalPos; 
    }

    void EndGame(){
        //Move this function elsewhere
    }
}

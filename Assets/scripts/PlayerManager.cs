using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{

    int lifes = 3;
    public int iceValue = 10;

    public CinemachineVirtualCamera camera;
    public int iceCount;
    int metalScrapCount;
    public GameObject playerPrefab;
    public GameObject planet;
    Vector3 position;
    GameObject player;
    public Planet myPlanet;
    public GameManager gameManager;

    public bool invulnerable;


    void Start()
    {
        invulnerable = false;
        player = Instantiate(playerPrefab, new Vector3(0,31,0), Quaternion.identity) as GameObject;
        player.GetComponent<Movement>().earth = planet;
        player.GetComponent<Movement>().Manager = this;
        camera.m_Follow =player.GetComponent<Movement>().cameraTracker.transform;
        Planet myPlanet = planet.GetComponent<Planet>();

        iceCount = 0;
        metalScrapCount = 0;
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(invulnerable){
            player.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else{
            player.GetComponent<SpriteRenderer>().color = Color.white;
        }
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
   
    public void playerPickIce(){
        iceCount += 1*iceValue;
    }

    public void playerPickScrap(){
        metalScrapCount += 1;
    }

    public void playerSellIce() {
        myPlanet.plusHealth(iceCount);
        iceCount = 0;
    }
    

    IEnumerator RespawnPlayer(){
        invulnerable = true;
        Debug.Log("Respawn player");
        position = ClosestAvailablePosition(transform.position);
        Destroy(player);
        yield return new WaitForSeconds(0.5f);
        player = Instantiate(playerPrefab, position, Quaternion.identity) as GameObject;
        player.GetComponent<Movement>().earth = planet;
        player.GetComponent<Movement>().Manager = this;
        camera.m_Follow = player.GetComponent<Movement>().cameraTracker.transform;
        yield return new WaitForSeconds(3f);
        invulnerable = false;
        yield return null;
    }

    Vector3 ClosestAvailablePosition(Vector3 position){
        Vector3 finalPos;
        finalPos = position + new Vector3(Random.Range(-1,1),32,0);
        return finalPos; 
    }

    void EndGame(){
        gameManager.EndGame();
    }
}
